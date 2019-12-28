using Global;
using Servico.Implementacao.Autenticacao.Comandos.AtualizarSenha;
using System.Threading.Tasks;

namespace Servico.Implementacao.Autenticacao.Contratos
{
    public interface IAtualizarSenhaService 
    {
        Task<Resposta<string>> Atualizar(AtualizarSenhaComando comando);
    }
}
