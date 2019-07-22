using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Servico.Contratos;
using System.Threading.Tasks;
using Global.Extensions;
using Global;
using Servico.Extensions;
using Repositorio.Contratos;
using System;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioServico : BaseService<UsuarioViewModel, Usuario>, IUsuarioService
    {
        public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper) : base(repositorio, mapper, "Usuário")
        { }

        public async Task<Resposta<string>> Autenticar(LoginViewModel viewModel)
        {
            var resultado = await GetRepositorio().RetornarUsuarioPorLoginESenha(viewModel.Login, viewModel.Senha);
               
            if (resultado == null)
                return new Resposta<string>(null, "Usuário e/ou senha inválidos!");

            string token = TokenGeradorHelper.Gerar(resultado);

            return new Resposta<string>(token);

        }

        public override async Task<UsuarioViewModel> ValidarInsercao(UsuarioViewModel viewModel)
        {
            var mensagem = await ValidarUsuarioComMesmoLoginOuEmail(viewModel);

            if (!string.IsNullOrEmpty(mensagem))
                throw new Exception(mensagem);

            viewModel.Senha = viewModel.Senha.ToMD5();

            return viewModel;
        }
        
        public override async Task<UsuarioViewModel> ValidarEdicao(UsuarioViewModel viewModel)
        {

            var mensagem = await ValidarUsuarioComMesmoLoginOuEmail(viewModel);

            if (!string.IsNullOrEmpty(mensagem))
                throw new Exception(mensagem);

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
            var msmLogin = await _repositorio.Listar(lnq => lnq.Login.IgualA(usuario.Login)) != null;

            if (msmLogin)
                return $"Login informado já está em uso!";
            
            var msmEmail = await _repositorio.Listar(lnq => lnq.Email.IgualA(usuario.Email)) != null;

            if (msmEmail)
                return $"E-mail informado já está em uso!";

            return string.Empty;
        }

    }
}
