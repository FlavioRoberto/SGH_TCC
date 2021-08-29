using FluentValidation;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Blocos.Base
{
    public abstract class  BlocoComandoValidadorBase<T> : Validador<T> where T : BlocoComandoBase
    {
        public BlocoComandoValidadorBase()
        {
            RuleFor(lnq => lnq.Descricao).NotEmpty().WithMessage("O campo descricão não pode estar vazio.");
        }
    }
}
