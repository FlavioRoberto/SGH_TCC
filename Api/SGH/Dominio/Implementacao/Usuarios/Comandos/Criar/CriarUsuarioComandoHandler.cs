using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Services.Helpers;
using SGH.Dominio.Core.Events;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoHandler : IRequestHandler<CriarUsuarioComando, Resposta<Usuario>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IMediator _mediator;
        private readonly IValidador<CriarUsuarioComando> _validador;
               
        public CriarUsuarioComandoHandler(IUsuarioRepositorio repositorio, IValidador<CriarUsuarioComando> validador, IMediator mediator)
        {
            _repositorio = repositorio;
            _validador = validador;
            _mediator = mediator;
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
                Telefone = request.Telefone,
                CursoCodigo = request.CursoCodigo
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

            await _mediator.Send(new EnviarEmailEvent(email, assunto, mensagem));
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
