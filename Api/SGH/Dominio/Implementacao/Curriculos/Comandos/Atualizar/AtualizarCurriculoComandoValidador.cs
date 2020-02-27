using FluentValidation;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Atualizar
{
    public class AtualizarCurriculoComandoValidador : AbstractValidator<AtualizarCurriculoComando>, IAtualizarCurriculoComandoValidador
    {
        public AtualizarCurriculoComandoValidador()
        {
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O código do currículo não pode ser vazio.");
        }
    }
}
