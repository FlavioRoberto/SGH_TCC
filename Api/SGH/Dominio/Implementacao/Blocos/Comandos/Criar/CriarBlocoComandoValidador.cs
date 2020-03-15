using FluentValidation;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar
{
    public class CriarBlocoComandoValidador : AbstractValidator<CriarBlocoComando>, IValidador<CriarBlocoComando>
    {
        public CriarBlocoComandoValidador()
        {
            RuleFor(lnq => lnq.Descricao).NotEmpty().WithMessage("O campo descricão não pode estar vazio.");
        }
    }
}
