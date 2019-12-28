using Servico.Contratos;

namespace Servico.Implementacao.Autenticacao.Comandos.AtualizarSenha
{
    public class AtualizarSenhaComando : IComando
    {
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
    }
}
