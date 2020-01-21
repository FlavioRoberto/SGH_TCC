using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Turnos.Comandos.Remover
{
    public class RemoverTurnoComandoValidador : AbstractValidator<RemoverTurnoComando>, ITurnoValidador
    {
        private readonly ITurnoRepositorio _repositorio;

        public RemoverTurnoComandoValidador(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
            
            RuleFor(lnq => lnq.TurnoId).NotEmpty().WithMessage("O código do turno não pode ser vazio");
            RuleFor(lnq => lnq.TurnoId).MustAsync(ValdiarTurnoExistente).WithMessage(lnq => $"Não foi encontrado um turno com o código {lnq.TurnoId}.");
        }

        private async Task<bool> ValdiarTurnoExistente(int turnoId, CancellationToken cancellationToken)
        {
            return await _repositorio.Contem(lnq => lnq.Codigo == turnoId);
        }
    }
}
