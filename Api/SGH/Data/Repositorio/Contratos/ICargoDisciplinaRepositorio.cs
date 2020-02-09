using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICargoDisciplinaRepositorio
    {
        Task<CargoDisciplina> Criar(CargoDisciplina entidade);
    }
}
