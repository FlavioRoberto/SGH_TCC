using Global;
using Servico.Implementacao.Autenticacao.Comandos.Login;
using System.Threading.Tasks;

namespace Servico.Implementacao.Autenticacao.Contratos
{
    public interface IAutenticacaoService
    {
        Task<Resposta<string>> Autenticar(LoginComando login);
    }
}
