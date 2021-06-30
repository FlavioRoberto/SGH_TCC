using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Base;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Lancar
{
    public class LancarAulasComandoValidador : AulaComandoBaseValidador<LancarAulasComando>, IValidador<LancarAulasComando>
    {
        public LancarAulasComandoValidador(ISalaRepositorio salaRepositorio,
                                           IHorarioAulaRepositorio horarioAulaRepositorio,
                                           ICargoDisciplinaRepositorio cargoDisciplinaRepositorio)
                                          :base(salaRepositorio, horarioAulaRepositorio, cargoDisciplinaRepositorio)
        {
            RuleFor(lnq => lnq.Reservas)
                .NotEmpty()
                .WithMessage("Não foram informadas reservas.");
        }
    }
}
