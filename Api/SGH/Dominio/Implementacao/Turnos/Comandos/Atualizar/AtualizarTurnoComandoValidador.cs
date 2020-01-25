using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;

using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComandoValidador : AbstractValidator<AtualizarTurnoComando>, IAtualizarTurnoComandoValidador
    {
        private readonly ITurnoRepositorio _repositorio;

        public AtualizarTurnoComandoValidador(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.TurnoId).NotEmpty().WithMessage("O código do turno não pode ser vazio.");
            RuleFor(lnq => lnq.Descricao).NotEmpty().WithMessage("O campo descrição não pode ser vazio.");
            RuleFor(lnq => lnq.TurnoId).MustAsync(ValidarTurnoExistente).WithMessage(lnq => $"Não foi encontrado um turno com o código {lnq.TurnoId}.");
        }

        private async Task<bool> ValidarTurnoExistente(int turnoId, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Contem(lnq => lnq.Codigo == turnoId);
            return resultado;
        }
    }
}
