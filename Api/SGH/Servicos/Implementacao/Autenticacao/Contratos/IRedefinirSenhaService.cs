using Global;
using Servico.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using System.Threading.Tasks;

namespace Servico.Implementacao.Autenticacao.Contratos
{
    public interface IRedefinirSenhaService
    {
        Task<Resposta<string>> Redefinir(RedefinirSenhaComando comando);
    }
}
