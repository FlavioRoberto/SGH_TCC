using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IBlocoRepositorio
    {
        Task<Bloco> Criar(Bloco entidade);
    }
}
