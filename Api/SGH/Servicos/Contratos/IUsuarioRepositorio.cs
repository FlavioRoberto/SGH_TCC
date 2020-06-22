using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Contratos
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>, IRepositorioPaginacao<Usuario>
    {
        Task<Usuario> RetornarUsuarioPorLoginESenha(string login, string senha);
        Task<int> QuantidadeUsuarioAdm();
    }
}
