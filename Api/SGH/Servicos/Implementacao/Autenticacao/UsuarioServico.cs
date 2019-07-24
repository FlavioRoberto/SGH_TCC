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
using System.Text;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioServico : BaseService<UsuarioViewModel, Usuario>, IUsuarioService
    {
        private UserResolverService _userResolver;
        private readonly IEmailSender _emailSender;

        public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper, IUserResolverService userResolver, IEmailSender emailSender) : base(repositorio, mapper, "Usuário")
        {
            _userResolver = userResolver as UserResolverService;
            _emailSender = emailSender;
        }

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

            string senha = GerarSenha();
            viewModel.Senha = senha.ToMD5();

            await EnviarEmail(viewModel, senha);

            return viewModel;
        }

        public override async Task<long> ValidarRemocao(long id)
        {

            var quantidadeUsuariosAdm = await (_repositorio as IUsuarioRepositorio).QuantidadeUsuarioAdm();
            var usuarioARemover = await _repositorio.Listar(lnq => lnq.Codigo == id);
            var usuarioAdm = usuarioARemover.Perfil.Administrador == true;

            if (usuarioAdm && quantidadeUsuariosAdm <= 1)
                throw new Exception("Não é possível remover o usuário, pois não existem outros usuários administradores!");

            return id;
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
            var codigoUsuarioLogado = _userResolver.GetUser().ToInt();
            
            var msmLogin = await _repositorio.Listar(lnq => lnq.Login.IgualA(usuario.Login)
                                 && codigoUsuarioLogado != lnq.Codigo) != null;

            if (msmLogin)
                return $"Login informado já está em uso!";
            
            var msmEmail = await _repositorio
                                .Listar(lnq => lnq.Email.IgualA(usuario.Email) 
                                 && lnq.Codigo != codigoUsuarioLogado) != null;

            if (msmEmail)
                return $"E-mail informado já está em uso!";

            return string.Empty;
        }

        private string GerarSenha()
        {
            string codigoSenha = DateTime.Now.Ticks.ToString();
            return BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider()
                .ComputeHash(Encoding.Default.GetBytes(codigoSenha))).Replace("-", String.Empty).Substring(0,35);

        }

        private async Task EnviarEmail(UsuarioViewModel viewModel, string senha)
        {
            string mensagem = $@"Seu cadastro no SGH foi realizado com sucesso! <br>
                                Usuário: {viewModel.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await _emailSender.SendEmailAsync(viewModel.Email, "Cadastro de novo usuário no SGH", mensagem);
        }
    }
}
