using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Services;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<string> RetornarProfessor(Aula aula)
        {
            var codigosCargos = new List<long> {
                aula.Disciplina.CodigoCargo
            };

            codigosCargos.AddRange(aula.DisciplinasAuxiliar.Select(x => x.Disciplina.CodigoCargo));

            var cargos = await _cargoRepositorio.Listar(codigosCargos);

            var descricao = new List<string>();

            if (cargos == null || !cargos.Any())
                return "Cargo não encontrado";

            foreach (var cargo in cargos)
            {
                var professor = cargo.Professor;

                if (professor != null)
                    descricao.Add(professor.Nome);
                else
                    descricao.Add($"Cargo {cargo.Numero}");
            }

            return string.Join(",", descricao);
        }
    }
}