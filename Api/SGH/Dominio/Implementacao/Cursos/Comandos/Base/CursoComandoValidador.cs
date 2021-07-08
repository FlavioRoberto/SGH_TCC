using FluentValidation;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Base
{
    public abstract class CursoComandoValidador<T> : Validador<T> where T : CursoComando
    {
        public CursoComandoValidador()
        {
            RuleFor(lnq => lnq.Descricao)
                .NotEmpty()
                .WithMessage("Descrição não foi informada.");
        }
    }
}
