using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarTodas
{
    public class ListarTodasDisciplinasCargoConsultaValidador : Validador<ListarTodasDisciplinasCargoConsulta>
    {
        private readonly ICargoRepositorio _cargoRepositorio;

        public ListarTodasDisciplinasCargoConsultaValidador(ICargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;

            RuleFor(lnq => lnq.CodigoCargo).GreaterThan(0).WithMessage("O campo código não pode ter valor menor ou igual a 0.");

            When(lnq => lnq.CodigoCargo > 0, () => {
                RuleFor(lnq => lnq.CodigoCargo).MustAsync(ValidarSeCargoExiste).WithMessage(c => $"Não foi encontrado um cargo com código {c.CodigoCargo}.");
            });
        }

        private async Task<bool> ValidarSeCargoExiste(int codigoCargo, CancellationToken arg2)
        {
            return await _cargoRepositorio.Contem(lnq => lnq.Codigo == codigoCargo);
        }
    }
}
