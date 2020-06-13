using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioGeral
{
    public class GerarRelatorioHorarioGeralConsultaValidador : AbstractValidator<GerarHorarioGeralRelatorioConsulta>, IValidador<GerarHorarioGeralRelatorioConsulta>
    {
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly ITurnoRepositorio _turnoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;

        public GerarRelatorioHorarioGeralConsultaValidador(ICurriculoRepositorio curriculoRepositorio,
                                                           ITurnoRepositorio turnoRepositorio,
                                                           ICursoRepositorio cursoRepositorio)
        {
            _curriculoRepositorio = curriculoRepositorio;
            _turnoRepositorio = turnoRepositorio;
            _cursoRepositorio = cursoRepositorio;

            RuleFor(lnq => lnq.Ano)
                .NotEmpty()
                .WithMessage("O ano não foi informado.");

            RuleFor(lnq => lnq.Semestre)
                .NotEmpty()
                .WithMessage("O semestre não foi informado.");

            RuleFor(lnq => lnq.CursoId)
                .NotEmpty()
                .WithMessage("O código do curriculo não foi informado.")

                .MustAsync(ValidarSeCursoExiste)
                .WithMessage(c => $"Não foi encontrado um curso com o código {c.CursoId}.")

                .MustAsync(ValidarSeCursoPossuiCurriculoVinculado)
                .WithMessage(c => $"Não foi encontrado currículos para o curso de código {c.CursoId}.");

            RuleFor(lnq => lnq.TurnoId)
                .NotEmpty()
                .WithMessage("O turno não foi informado.")

                .MustAsync(ValidarSeTurnoExiste)
                .WithMessage(c => $"Não foi encontrado um turno com o código {c.TurnoId}.");
        }

        private async Task<bool> ValidarSeCursoPossuiCurriculoVinculado(int codigoCurso, CancellationToken arg2)
        {
            return await _curriculoRepositorio.Contem(lnq => lnq.CodigoCurso == codigoCurso);
        }

        private async Task<bool> ValidarSeTurnoExiste(int codigoTurno, CancellationToken arg2)
        {
            return await _turnoRepositorio.Contem(lnq => lnq.Codigo == codigoTurno);
        }

        private async Task<bool> ValidarSeCursoExiste(int codigoCurso, CancellationToken arg2)
        {
            return await _cursoRepositorio.Contem(lnq => lnq.Codigo == codigoCurso);
        }
    }
}
