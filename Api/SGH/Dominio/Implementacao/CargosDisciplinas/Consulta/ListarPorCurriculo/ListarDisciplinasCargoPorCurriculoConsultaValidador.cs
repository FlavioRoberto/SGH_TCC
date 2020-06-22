using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo
{
    public class ListarDisciplinasCargoPorCurriculoConsultaValidador : AbstractValidator<ListarDisciplinaCargoPorCurriculoConsulta>, IValidador<ListarDisciplinaCargoPorCurriculoConsulta>
    {
        private readonly ITurnoRepositorio _turnoRepositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;

        public ListarDisciplinasCargoPorCurriculoConsultaValidador(ICurriculoRepositorio curriculoRepositorio, 
                                                                   ITurnoRepositorio turnoRepositorio )
        {
            _curriculoRepositorio = curriculoRepositorio;
            _turnoRepositorio = turnoRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.Ano)
                .NotEmpty()
                .WithMessage("O campo ano não foi informado.");

            RuleFor(lnq => lnq.Periodo)
                .NotEmpty()
                .WithMessage("O campo período não foi informado.");

            RuleFor(lnq => lnq.Semestre)
                .NotEmpty()
                .WithMessage("O campo semestre não foi informado.");

            RuleFor(lnq => lnq.CodigoTurno)
                .NotEmpty()
                .WithMessage("O campo código do turno não foi informado.")

                .MustAsync(ValidarSeTurnoExiste)
                .WithMessage(c => $"Não foi encontrado um turno com o código {c.CodigoTurno}.");

            RuleFor(lnq => lnq.CodigoCurriculo)
                .NotEmpty()
                .WithMessage("O campo código do currículo não foi informado.")
                
                .MustAsync(ValidarSeCurriculoExiste)
                .WithMessage(c => $"Não foi encontrado um currículo com o código {c.CodigoCurriculo}.");
        }

        private async Task<bool> ValidarSeCurriculoExiste(int codigoCurriculo, CancellationToken arg2)
        {
            return await _curriculoRepositorio.Contem(lnq => lnq.Codigo == codigoCurriculo);
        }

        private async Task<bool> ValidarSeTurnoExiste(int codigoTurno, CancellationToken arg2)
        {
            return await _turnoRepositorio.Contem(lnq => lnq.Codigo == codigoTurno);
        }
    }
}
