using AutoMapper;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Aplicacao.Implementacao.Usuarios
{
    public class UsuarioServico : IUsuarioService
    {
        private IUsuarioResolverService _userResolver;
        private readonly IEmailService _emailService;
        private readonly IRepositorio<Usuario> _repositorio;
        private readonly IMapper _mapper;

        public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper, IUsuarioResolverService userResolver, IEmailService emailSender)
        {
            _userResolver = userResolver ;
            _emailService = emailSender;
            _repositorio = repositorio;
            _mapper = mapper;
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

        public async Task<UsuarioViewModel> ValidarInsercao(UsuarioViewModel viewModel)
        {
            var mensagem = await ValidarUsuarioComMesmoLoginOuEmail(viewModel);

            if (!string.IsNullOrEmpty(mensagem))
                throw new ValidacaoException(mensagem);

            string senha = SenhaHelper.Gerar();
            viewModel.Senha = senha.ToMD5();

            mensagem = $@"Seu cadastro no SGH foi realizado com sucesso! <br>
                                Usuário: {viewModel.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await _emailService.SendEmailAsync(viewModel.Email, "Cadastro de novo usuário no SGH", mensagem);

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

    }
}
