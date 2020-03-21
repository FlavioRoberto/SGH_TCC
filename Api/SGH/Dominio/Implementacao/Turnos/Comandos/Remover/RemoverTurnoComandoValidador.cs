using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover
{
    public class RemoverTurnoComandoValidador : AbstractValidator<RemoverTurnoComando>, IValidador<RemoverTurnoComando>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;

        private CargoDisciplina _cargoDisciplina;

        public RemoverTurnoComandoValidador(ITurnoRepositorio repositorio,
                                            ICargoDisciplinaRepositorio cargoDisciplinaRepositorio)
        {
            _repositorio = repositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;

            RuleFor(lnq => lnq.TurnoId).NotEmpty().WithMessage("O código do turno não pode ser vazio");
            RuleFor(lnq => lnq.TurnoId).MustAsync(ValdiarTurnoExistente).WithMessage(lnq => $"Não foi encontrado um turno com o código {lnq.TurnoId}.");

            When(lnq => lnq.TurnoId > 0, () => {
                RuleFor(lnq => lnq.TurnoId).MustAsync(VerificarSeTurnoTemVinculoComDisciplinaDeCargo).WithMessage(c => $"Não foi possível remover o turno pois ele está vinculado a disciplina do cargo de código {_cargoDisciplina.CodigoCargo}.");
            });
        }

        private async Task<bool> VerificarSeTurnoTemVinculoComDisciplinaDeCargo(int codigoTurno, CancellationToken arg2)
        {
            _cargoDisciplina = await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.CodigoTurno == codigoTurno);
            return _cargoDisciplina == null ? true : false;
        }

        private async Task<bool> ValdiarTurnoExistente(int turnoId, CancellationToken cancellationToken)
        {
            return await _repositorio.Contem(lnq => lnq.Codigo == turnoId);
        }
    }
}
