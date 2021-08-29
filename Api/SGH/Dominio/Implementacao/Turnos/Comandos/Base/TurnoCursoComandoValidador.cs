using FluentValidation;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Base
{
    public abstract class TurnoCursoComandoValidador<T> : Validador<T> where T : TurnoComando
    {
        public TurnoCursoComandoValidador()
        {
            RuleFor(lnq => lnq.Descricao)
                .NotEmpty()
                .WithMessage("O campo descrição não pode ser vazio.");

            RuleFor(lnq => lnq.Horarios)
                .NotEmpty()
                .WithMessage("Não foram informados horários para o turno");
        }
    }
}
