using FluentValidation;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Atualizar
{
    public class AtualizarCurriculoComandoValidador : AbstractValidator<AtualizarCurriculoComando>, IAtualizarCurriculoComandoValidador
    {
        public AtualizarCurriculoComandoValidador()
        {
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O código do currículo não pode ser vazio.");
        }
    }
}
