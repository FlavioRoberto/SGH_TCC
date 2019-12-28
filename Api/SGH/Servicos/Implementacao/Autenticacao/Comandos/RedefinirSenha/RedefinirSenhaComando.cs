using Servico.Contratos;

namespace Servico.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComando : IComando
    {
        public string Email { get; set; }
    }
}
