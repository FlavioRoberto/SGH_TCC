using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Comum
{
    public abstract class HorarioAulaComandoValidador<T> : Validador<T>, IValidador<T> where T : HorarioAulaComando
    {
        private readonly ITurnoRepositorio _turnoRepositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;

        public HorarioAulaComandoValidador(ITurnoRepositorio turnoRepositorio, ICurriculoRepositorio curriculoRepositorio)
        {
            _turnoRepositorio = turnoRepositorio;
            _curriculoRepositorio = curriculoRepositorio;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.Ano).NotEmpty().WithMessage("O campo ano não pode ser vazio.");
            RuleFor(lnq => lnq.Periodo).NotEmpty().WithMessage("O campo período não pode ser vazio.");
            RuleFor(lnq => lnq.Semestre).NotEmpty().WithMessage("O campo semestre não pode ser vazio.");

            RuleFor(lnq => lnq.CodigoCurriculo)
                .NotEmpty()
                .WithMessage("O campo código do currículo não pode ser vazio.")
                .DependentRules(() => RuleFor(lnq => lnq.CodigoCurriculo)
                                        .MustAsync(ValidarSeCurriculoExiste)
                                        .WithMessage(c => $"Não foi encontrado um currículo com o código {c.CodigoCurriculo}."));

            RuleFor(lnq => lnq.CodigoTurno)
                .NotEmpty()
                .WithMessage("O campo código do turno não pode ser vazio.")
                .DependentRules(() => RuleFor(lnq => lnq.CodigoTurno)
                                        .MustAsync(ValidarSeTurnoExiste)
                                        .WithMessage(c => $"Não foi encontrado um turno com o código {c.CodigoTurno}."));
        }

        private async Task<bool> ValidarSeTurnoExiste(int codigoTurno, CancellationToken arg2)
        {
            return await _turnoRepositorio.Contem(lnq => lnq.Codigo == codigoTurno);
        }

        private async Task<bool> ValidarSeCurriculoExiste(int codigoCurriculo, CancellationToken arg2)
        {
            return await _curriculoRepositorio.Contem(lnq => lnq.Codigo == codigoCurriculo);
        }
    }
}
