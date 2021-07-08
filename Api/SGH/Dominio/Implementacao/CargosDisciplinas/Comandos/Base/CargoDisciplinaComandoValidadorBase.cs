using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Base
{
    public class CargoDisciplinaComandoValidadorBase<T> : Validador<T> where T: CargoDisciplinaComandoBase
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly ITurnoRepositorio _turnoRepositorio;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;

        public CargoDisciplinaComandoValidadorBase(ICargoRepositorio cargoRepositorio,
                                                   ITurnoRepositorio turnoRepositorio,
                                                   ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
            _turnoRepositorio = turnoRepositorio;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;

            RuleFor(lnq => lnq.CodigoCargo).NotEmpty().WithMessage("O campo código do cargo não pode ser menor ou igual a 0.");
            RuleFor(lnq => lnq.CodigoCurriculoDisciplina).NotEmpty().WithMessage("O campo código da disciplina do currículo não pode ser menor ou igual a 0.");
            RuleFor(lnq => lnq.CodigoTurno).NotEmpty().WithMessage("O campo código do turno não pode ser menor ou igual a 0.");

            When(lnq => lnq.CodigoCargo > 0 && lnq.CodigoCurriculoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq.CodigoCargo).MustAsync(ValidarSeCargoExiste).WithMessage(c => $"Não foi encontrado um cargo de código {c.CodigoCargo}.");
                RuleFor(lnq => lnq.CodigoCurriculoDisciplina).MustAsync(ValidarSeCurriculoDisciplinaExiste).WithMessage(c => $"Não foi encontrado a disciplina de currículo com código {c.CodigoCurriculoDisciplina}.");
            });

            When(lnq => lnq.CodigoTurno > 0, () =>
            {
                RuleFor(lnq => lnq.CodigoTurno).MustAsync(ValidarSeTurnoExiste).WithMessage(c => $"Não foi encontrado um turno com código {c.CodigoTurno}.");
            });
        }

        private async Task<bool> ValidarSeCurriculoDisciplinaExiste(int codigoCurriculoDisciplina, CancellationToken arg2)
        {
            var curriculoDisciplina = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == codigoCurriculoDisciplina);
            return curriculoDisciplina != null;
        }

        private async Task<bool> ValidarSeCargoExiste(int codigoCargo, CancellationToken arg2)
        {
            return await _cargoRepositorio.Contem(lnq => lnq.Codigo == codigoCargo);
        }

        private async Task<bool> ValidarSeTurnoExiste(int codigoTurno, CancellationToken arg2)
        {
            return await _turnoRepositorio.Contem(lnq => lnq.Codigo == codigoTurno);
        }
    }
}
