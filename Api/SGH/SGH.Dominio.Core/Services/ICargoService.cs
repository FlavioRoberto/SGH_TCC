using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Services
{
    public interface ICargoService
    {
        Task<string> RetornarProfessor(Aula aula);
    }
}
