using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Comum;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Atualizar
{
    public class AtualizarHorarioAulaComandoValidador : HorarioAulaComandoValidador<AtualizarHorarioAulaComando>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;

        public AtualizarHorarioAulaComandoValidador(ITurnoRepositorio turnoRepositorio, 
                                                ICurriculoRepositorio curriculoRepositorio,
                                                IHorarioAulaRepositorio horarioAulaRepositorio) : base(turnoRepositorio, curriculoRepositorio)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;

            RuleFor(lnq => lnq.Codigo)
                .NotEmpty()
                .WithMessage("O campo códgo não pode ser vazio.")
                
                .MustAsync(ValidarSeHorarioExiste)
                .WithMessage(c => $"Não foi encontrado um horário com o código {c.Codigo}.");
        }

        private async Task<bool> ValidarSeHorarioExiste(int codigoHorario, CancellationToken arg2)
        {
            return await _horarioAulaRepositorio.Contem(lnq => lnq.Codigo == codigoHorario);
        }
    }
}
