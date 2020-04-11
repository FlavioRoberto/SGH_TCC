using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IHorarioAulaRepositorio
    {
        Task<HorarioAula> Criar(HorarioAula entidade);
    }
}
