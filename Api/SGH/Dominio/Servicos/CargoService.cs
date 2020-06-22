using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
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

        public async Task<string> RetornarProfessor(int codigoCargo)
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
