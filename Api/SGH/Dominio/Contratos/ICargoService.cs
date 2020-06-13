using System.Threading.Tasks;

namespace SGH.Dominio.Services.Contratos
{
    public interface ICargoService
    {
        Task<string> RetornarProfessor(int codigoCargo);
    }
}
