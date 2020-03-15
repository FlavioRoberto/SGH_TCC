using FluentValidation;

namespace SGH.Dominio.Services.Implementacao.Blocos.Base
{
    public abstract class  BlocoComandoValidadorBase<T> : AbstractValidator<T> where T : BlocoComandoBase
    {
        public BlocoComandoValidadorBase()
        {
            RuleFor(lnq => lnq.Descricao).NotEmpty().WithMessage("O campo descricão não pode estar vazio.");
        }
    }
}
