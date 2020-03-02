using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar
{
    public class CriarCargoDisciplinaComandoValidador : AbstractValidator<CriarCargoDisciplinaComando>, ICriarCargoDisciplinaComandoValidador
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly ITurnoRepositorio _turnoRepositorio;

        public CriarCargoDisciplinaComandoValidador(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, 
                                                    ICurriculoRepositorio curriculoRepositorio, 
                                                    ICargoRepositorio cargoRepositorio,
                                                    ITurnoRepositorio turnoRepositorio)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _curriculoRepositorio = curriculoRepositorio;
            _cargoRepositorio = cargoRepositorio;
            _turnoRepositorio = turnoRepositorio;

            RuleFor(lnq => lnq.CodigoCargo).NotEmpty().WithMessage("O campo código do cargo não pode ser menor ou igual a 0.");
            RuleFor(lnq => lnq.CodigoCurriculoDisciplina).NotEmpty().WithMessage("O campo código da disciplina do currículo não pode ser menor ou igual a 0.");
            RuleFor(lnq => lnq.CodigoTurno).NotEmpty().WithMessage("O campo código do turno não pode ser menor ou igual a 0.");

            When(lnq => lnq.CodigoCargo > 0 && lnq.CodigoCurriculoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq).MustAsync(ValidarSeCargoDisciplinaJaAdicionado).WithMessage("Já foi adicionado uma disciplina com os mesmos valores.");
                RuleFor(lnq => lnq.CodigoCargo).MustAsync(ValidarSeCargoExiste).WithMessage(c => $"Não foi encontrado um cargo de código {c.CodigoCargo}.");
                RuleFor(lnq => lnq.CodigoCurriculoDisciplina).MustAsync(ValidarSeCurriculoDisciplinaExiste).WithMessage(c => $"Não foi encontrado a disciplina de currículo com código {c.CodigoCurriculoDisciplina}.");
            });

            When(lnq => lnq.CodigoTurno > 0, () => {
                RuleFor(lnq => lnq.CodigoTurno).MustAsync(ValidarSeTurnoExiste).WithMessage(c => $"Não foi encontrado um turno com código {c.CodigoTurno}.");
            });
        }

        private async Task<bool> ValidarSeCargoDisciplinaJaAdicionado(CriarCargoDisciplinaComando comando, CancellationToken arg2)
        {
            var resultado = await _cargoDisciplinaRepositorio.Contem(lnq => lnq.CodigoCargo == comando.CodigoCargo && lnq.CodigoCurriculoDisciplina == comando.CodigoCurriculoDisciplina);
            return !resultado;
        }

        private async Task<bool> ValidarSeCurriculoDisciplinaExiste(int codigoCurriculoDisciplina, CancellationToken arg2)
        {
            var curriculoDisciplina = await _curriculoRepositorio.ConsultarCurriculoDisciplina(codigoCurriculoDisciplina);
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
