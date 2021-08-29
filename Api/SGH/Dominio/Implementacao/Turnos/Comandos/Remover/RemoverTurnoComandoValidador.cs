using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover
{
    public class RemoverTurnoComandoValidador : Validador<RemoverTurnoComando>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;

        private CargoDisciplina _cargoDisciplina;

        public RemoverTurnoComandoValidador(ITurnoRepositorio repositorio,
                                            ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                            IHorarioAulaRepositorio horarioAulaRepositorio)
        {
            _repositorio = repositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _horarioAulaRepositorio = horarioAulaRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.TurnoId)
                .NotEmpty()
                .WithMessage("O código do turno não pode ser vazio")

                .MustAsync(ValdiarTurnoExistente)
                .WithMessage(lnq => $"Não foi encontrado um turno com o código {lnq.TurnoId}.")

                .MustAsync(ValidarSeTurnoTemVinculoComDisciplinaDeCargo)
                .WithMessage(c => $"Não foi possível remover o turno pois ele está vinculado a disciplina do cargo de código {_cargoDisciplina.CodigoCargo}.")
                
                .MustAsync(ValidarSeTurnoEstaVinculadoEmHorario)
                .WithMessage("Não foi possível remover o turno pois ele está vinculado em horarios.");            
        }
        
        private async Task<bool> ValidarSeTurnoTemVinculoComDisciplinaDeCargo(int codigoTurno, CancellationToken arg2)
        {
            _cargoDisciplina = await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.CodigoTurno == codigoTurno);
            return _cargoDisciplina == null ? true : false;
        }

        private async Task<bool> ValdiarTurnoExistente(int turnoId, CancellationToken cancellationToken)
        {
            return await _repositorio.Contem(lnq => lnq.Codigo == turnoId);
        }

        private async Task<bool> ValidarSeTurnoEstaVinculadoEmHorario(int codigoTurno, CancellationToken arg2)
        {
            var vinculado = await _horarioAulaRepositorio.Contem(lnq => lnq.CodigoTurno == codigoTurno);

            if (vinculado)
                return false;

            return true;
        }
    }
}
