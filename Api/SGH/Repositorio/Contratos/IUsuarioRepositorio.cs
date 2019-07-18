using Dominio.Model.Autenticacao;
using System.Threading.Tasks;

namespace Repositorio.Contratos
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<Usuario> RetornarUsuarioPorLoginESenha(string login, string senha);
    }
}
