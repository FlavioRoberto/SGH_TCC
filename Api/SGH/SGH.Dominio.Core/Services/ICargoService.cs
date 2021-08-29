using System.Threading.Tasks;

namespace SGH.Dominio.Core.Services
{
    public interface ICargoService
    {
        Task<string> RetornarProfessor(long codigoCargo);
    }
}
