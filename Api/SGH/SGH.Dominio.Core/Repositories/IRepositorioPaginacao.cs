using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Repositories
{
    public interface IRepositorioPaginacao<T>
    {
        Task<Paginacao<T>> ListarPorPaginacao(Paginacao<T> entidadePaginada);
    }
}
