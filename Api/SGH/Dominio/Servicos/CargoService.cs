using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Services;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Servicos
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepositorio _cargoRepositorio;

        public CargoService(ICargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        public async Task<string> RetornarProfessor(long codigoCargo)
        {
            var cargo = await _cargoRepositorio.Consultar(lnq => lnq.Codigo == codigoCargo);

            if (cargo == null)
                return "Cargo não encontrado";

            var professor = await _cargoRepositorio.ConsultarProfessor(codigoCargo);

            if (professor != null)
                return professor.Nome;

            return $"Cargo {cargo.Numero}";
        }
    }
}
