using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICargoRepositorio : IRepositorioPaginacao<Cargo>
    {
        Task<Cargo> Criar(Cargo entidade);
    }
}
