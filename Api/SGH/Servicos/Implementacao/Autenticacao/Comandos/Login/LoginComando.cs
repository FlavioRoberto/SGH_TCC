using Aplicacao.Contratos;
using Global;
using MediatR;

namespace Aplicacao.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComando : IRequest<Resposta<string>>
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
