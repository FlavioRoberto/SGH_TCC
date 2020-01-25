using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComando : IRequest<Resposta<string>>
    {
        public string Email { get; set; }
    }
}
