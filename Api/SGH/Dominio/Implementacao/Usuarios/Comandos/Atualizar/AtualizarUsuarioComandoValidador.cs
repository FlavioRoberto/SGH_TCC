using FluentValidation;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComandoValidador : AbstractValidator<AtualizarUsuarioComando>, IAtualizarUsuarioComandoValidador
    {
        public AtualizarUsuarioComandoValidador()
        {
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código não pode ser vazio.");
        }
    }
}
