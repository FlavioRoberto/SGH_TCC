using FluentValidation;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Criar
{
    public class CriarCurriculoComandoValidador : AbstractValidator<CriarCurriculoComando>, ICriarCurriculoComandoValidador
    {
        public CriarCurriculoComandoValidador()
        {
            RuleFor(lnq => lnq.CodigoCurso).NotEmpty().WithMessage("Código do curso não foi informado.");
            RuleFor(lnq => lnq.Ano).NotEmpty().WithMessage("Ano não foi informado.");
            RuleFor(lnq => lnq.Disciplinas).NotEmpty().WithMessage("Disciplinas não foram informadas.");
        }
    }
}
