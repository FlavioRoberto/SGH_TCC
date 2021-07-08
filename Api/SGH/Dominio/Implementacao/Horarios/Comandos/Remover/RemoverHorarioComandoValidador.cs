using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Remover
{
    public class RemoverHorarioComandoValidador : Validador<RemoverHorarioComando>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;

        public RemoverHorarioComandoValidador(IHorarioAulaRepositorio horarioAulaRepositorio)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.CodigoHorario)
                .NotEmpty()
                .WithMessage("O código do horário não foi informado.")

                .MustAsync(ValidarSeHorarioExiste)
                .WithMessage(c => $"Não foi encontrado um horário com o código {c.CodigoHorario}.");
        }

        private async Task<bool> ValidarSeHorarioExiste(int codigoHorario, CancellationToken arg2)
        {
            return await _horarioAulaRepositorio.Contem(lnq => lnq.Codigo == codigoHorario);
        }
    }
}
