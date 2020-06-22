using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Consulta.ListarPorHorario
{
    public class ListarAulaPorHorarioValidador : AbstractValidator<ListarAulaPorHorarioConsulta>, IValidador<ListarAulaPorHorarioConsulta>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;

        public ListarAulaPorHorarioValidador(IHorarioAulaRepositorio horarioAulaRepositorio)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;
        }

        public ListarAulaPorHorarioValidador()
        {
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
