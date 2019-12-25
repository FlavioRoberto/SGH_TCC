﻿using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Servico.Contratos;
using System.Threading.Tasks;
using Global.Extensions;
using Global;
using Servico.Extensions;
using Repositorio.Contratos;
using System;
using System.Text;
using Servico.Exceptions;
using Api.Servicos.Email;
using Repositorio;
using Dominio.ViewModel;
using System.Collections.Generic;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioServico : IUsuarioService
    {
        private UsuarioResolverService _userResolver;
        private readonly IEmailService _emailService;
        private readonly IRepositorio<Usuario> _repositorio;
        private readonly IMapper _mapper;

        public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper, IUsuarioResolverService userResolver, IEmailService emailSender)
        {
            _userResolver = userResolver as UsuarioResolverService;
            _emailService = emailSender;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public virtual async Task<Resposta<UsuarioViewModel>> Atualizar(UsuarioViewModel entidadeViewModel)
        {
            try
            {
                entidadeViewModel = await ValidarEdicao(entidadeViewModel);
                var entidade = _mapper.Map<Usuario>(entidadeViewModel);
                var resultado = _mapper.Map<UsuarioViewModel>(await _repositorio.Atualizar(entidade));
                return new Resposta<UsuarioViewModel>(resultado);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ValidacaoException))
                    return new Resposta<UsuarioViewModel>(entidadeViewModel, e.Message);

                return new Resposta<UsuarioViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar o usuário: {e.Message}");
            }

        }

        public virtual async Task<Resposta<UsuarioViewModel>> Criar(UsuarioViewModel entidade)
        {
            try
            {
                entidade = await ValidarInsercao(entidade);
                var resultado = await _repositorio.Criar(_mapper.Map<Usuario>(entidade));
                return new Resposta<UsuarioViewModel>(_mapper.Map<UsuarioViewModel>(resultado));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ValidacaoException))
                    return new Resposta<UsuarioViewModel>(entidade, e.Message);

                return new Resposta<UsuarioViewModel>(entidade, $"Ocorreu um erro ao criar o usuário: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<UsuarioViewModel>>> ListarComPaginacao(Paginacao<UsuarioViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Usuario>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<UsuarioViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<UsuarioViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<UsuarioViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<UsuarioViewModel>>(null, $"Ocorreu um erro ao listar o usuário: {e.Message}");
            }
        }

        public async Task<Resposta<List<UsuarioViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<UsuarioViewModel>>(_mapper.Map<List<UsuarioViewModel>>(resultado));
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {
                id = await ValidarRemocao(id);

                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);

                return new Resposta<bool>(false, $"Não foi possível remover o usuário!");

            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ValidacaoException))
                    return new Resposta<bool>(false, e.Message);

                return new Resposta<bool>(false, $"Não foi possível remover o usuário: {e.Message}!");

            }
        }

        public async Task<Resposta<string>> Autenticar(LoginViewModel viewModel)
        {
            var resultado = await GetRepositorio().RetornarUsuarioPorLoginESenha(viewModel.Login, viewModel.Senha);

            if (resultado == null)
                return new Resposta<string>(null, "Usuário e/ou senha inválidos!");

            if (!resultado.Ativo)
                return new Resposta<string>(null, "Não foi possível logar no sistema, o usuário informado está inativo!");

            string token = TokenGeradorHelper.Gerar(resultado);

            return new Resposta<string>(token);

        }

        public async Task<Resposta<string>> RedefinirSenha(string email)
        {
            var usuario = await GetRepositorio().Listar(lnq => lnq.Email.Equals(email));

            if (usuario == null)
                return new Resposta<string>(null, $"Não foi encontrado um usuário vinculado com o e-mail {email}!");

            string senha = GerarSenha();
            usuario.Senha = senha.ToMD5();
            await _repositorio.Atualizar(usuario);

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);

            string mensagem = mensagem = $@"Sua senha no SGH foi redefinida com sucesso! <br>
                                Usuário: {usuario.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await EnviarEmail(usuarioViewModel, "Redefinição de senha no SGH", mensagem);
            return new Resposta<string>("Senha redefinida com sucesso! Foi enviado um e-mail com seus dados de acesso.");
        }

        public async Task<Resposta<string>> AtualizarSenha(string senha, string novaSenha)
        {
            var codigoUsuarioLogado = _userResolver.GetUser().ToInt();

            var usuario = await _repositorio.Listar(lnq => lnq.Codigo == codigoUsuarioLogado);

            if (usuario == null)
                return new Resposta<string>(null, "Usuário não encontrado!");

            if (!usuario.Senha.Equals(senha.ToMD5()))
                return new Resposta<string>(null, "Senha incorreta!");

            usuario.Senha = novaSenha.ToMD5();

            await _repositorio.Atualizar(usuario);

            return new Resposta<string>("A senha foi atualizada!");

        }

        public async Task<UsuarioViewModel> ValidarInsercao(UsuarioViewModel viewModel)
        {
            var mensagem = await ValidarUsuarioComMesmoLoginOuEmail(viewModel);

            if (!string.IsNullOrEmpty(mensagem))
                throw new ValidacaoException(mensagem);

            string senha = GerarSenha();
            viewModel.Senha = senha.ToMD5();

            mensagem = $@"Seu cadastro no SGH foi realizado com sucesso! <br>
                                Usuário: {viewModel.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await EnviarEmail(viewModel, "Cadastro de novo usuário no SGH", mensagem);

            return viewModel;
        }

        public async Task<long> ValidarRemocao(long id)
        {

            var quantidadeUsuariosAdm = await (_repositorio as IUsuarioRepositorio).QuantidadeUsuarioAdm();
            var usuarioARemover = await _repositorio.Listar(lnq => lnq.Codigo == id);
            var usuarioAdm = usuarioARemover.Perfil.Administrador == true;

            if (usuarioAdm && quantidadeUsuariosAdm <= 1)
                throw new ValidacaoException("Não é possível remover o usuário, pois não existem outros usuários administradores!");

            return id;
        }

        public async Task<UsuarioViewModel> ValidarEdicao(UsuarioViewModel viewModel)
        {

            var mensagem = await ValidarUsuarioComMesmoLoginOuEmail(viewModel);

            if (!string.IsNullOrEmpty(mensagem))
                throw new ValidacaoException(mensagem);

            var usuarioBanco = await _repositorio.Listar(lnq => lnq.Codigo == viewModel.Codigo);

            if (usuarioBanco == null)
                return viewModel;

            if (!usuarioBanco.Senha.IgualA(viewModel.Senha))
                viewModel.Senha = viewModel.Senha.ToMD5();

            return viewModel;
        }


        private IUsuarioRepositorio GetRepositorio()
        {
            return _repositorio as IUsuarioRepositorio;
        }

        private async Task<string> ValidarUsuarioComMesmoLoginOuEmail(UsuarioViewModel usuario)
        {
            // var codigoUsuarioLogado = _userResolver.GetUser().ToInt();

            var msmLogin = await _repositorio.Listar(lnq => lnq.Login.IgualA(usuario.Login)
                                 && usuario.Codigo != lnq.Codigo) != null;

            if (msmLogin)
                return $"Login informado já está em uso!";

            var msmEmail = await _repositorio
                                .Listar(lnq => lnq.Email.IgualA(usuario.Email)
                                 && lnq.Codigo != usuario.Codigo) != null;

            if (msmEmail)
                return $"E-mail informado já está em uso!";

            return string.Empty;
        }

        private string GerarSenha()
        {
            string codigoSenha = DateTime.Now.Ticks.ToString();
            return BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider()
                .ComputeHash(Encoding.Default.GetBytes(codigoSenha))).Replace("-", String.Empty).Substring(0, 35);
        }

        private async Task EnviarEmail(UsuarioViewModel viewModel, string assunto, string mensagem)
        {
            await _emailService.SendEmailAsync(viewModel.Email, assunto, mensagem);
        }
    }
}
