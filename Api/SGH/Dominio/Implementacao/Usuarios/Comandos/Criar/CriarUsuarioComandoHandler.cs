using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Services.Helpers;
using SGH.Dominio.Services.Email;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoHandler : IRequestHandler<CriarUsuarioComando, Resposta<Usuario>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IValidador<CriarUsuarioComando> _validador;
        private readonly IEmailService _emailService;
               
        public CriarUsuarioComandoHandler(IUsuarioRepositorio repositorio, IValidador<CriarUsuarioComando> validador, IEmailService emailService)
        {
            _repositorio = repositorio;
            _validador = validador;
            _emailService = emailService;
        }

        public async Task<Resposta<Usuario>> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Usuario>(erros);

            var usuario = new Usuario
            {
                Ativo = request.Ativo,
                Email = request.Email,
                Foto = request.Foto,
                Login = request.Login,
                Nome = request.Nome,
                PerfilCodigo = request.PerfilCodigo,
                Telefone = request.Telefone
            };

            string senha = SenhaHelper.Gerar();

            usuario.Senha = senha.ToMD5();

            await EnviarEmailConfirmacaoCadastro(usuario.Email, usuario.Login, senha);

            var usuarioCadastrado = await _repositorio.Criar(usuario);

            return new Resposta<Usuario>(usuarioCadastrado);

        }

        public async Task EnviarEmailConfirmacaoCadastro(string email, string login, string senha)
        {
            var mensagem = GerarEmailMensagem(login, senha);

            var assunto = GerarEmailAssunto();

            await _emailService.Enviar(email, assunto, mensagem);
        }

        public string GerarEmailAssunto()
        {
            return "Cadastro de novo usuário no SGH";
        }

        public string GerarEmailMensagem(string login, string senha)
        {
            return $@"Seu cadastro no SGH foi realizado com sucesso! <br>
                                Usuário: {login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";
        }
    }
}
