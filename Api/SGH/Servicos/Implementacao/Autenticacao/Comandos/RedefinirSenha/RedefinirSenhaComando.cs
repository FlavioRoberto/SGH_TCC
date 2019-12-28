using Aplicacao.Contratos;
using Global;
using MediatR;

namespace Aplicacao.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComando : IRequest<Resposta<string>>
    {
        public string Email { get; set; }
    }
}
