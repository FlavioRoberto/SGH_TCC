using System.Threading.Tasks;
using MediatR;
using System.Threading;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Helpers;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Core.Events;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComandoHandler : IRequestHandler<RedefinirSenhaComando, Resposta<string>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IMediator _mediator;
        private readonly IValidador<RedefinirSenhaComando> _validador;

        public RedefinirSenhaComandoHandler(IUsuarioRepositorio repositorio, IMediator emailService, IValidador<RedefinirSenhaComando> validador)
        {
            _repositorio = repositorio;
            _mediator = emailService;
            _validador = validador;
        }

        public async Task<Resposta<string>> Handle(RedefinirSenhaComando request, CancellationToken cancellationToken)
        {

            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>(null, erros);

            var email = request.Email;
            var usuario = await _repositorio.Consultar(lnq => lnq.Email.Equals(email));

            string senha = SenhaHelper.Gerar();
            usuario.Senha = senha.ToMD5();
            await _repositorio.Atualizar(usuario);

            string mensagem = mensagem = $@"Sua senha no SGH foi redefinida com sucesso! <br>
                                Usuário: {usuario.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await _mediator.Send(new EnviarEmailEvent(usuario.Email, "Redefinição de senha no SGH", mensagem));

            return new Resposta<string>("Senha redefinida com sucesso! Foi enviado um e-mail com seus dados de acesso.", "");
        }
    }
}
