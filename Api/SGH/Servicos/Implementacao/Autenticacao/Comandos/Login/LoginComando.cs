using Servico.Contratos;

namespace Servico.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComando : IComando
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
