using MediatR;
using SGH.Dominio.Core;
using System.ComponentModel.DataAnnotations;

namespace SGH.Dominio.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComando : IRequest<Resposta<string>>
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
