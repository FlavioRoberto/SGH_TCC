using FluentValidation;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComandoValidador : AbstractValidator<AtualizarUsuarioComando>, IAtualizarUsuarioComandoValidador
    {
        private IUsuarioComandoValidador _validadorUsuario;

        public AtualizarUsuarioComandoValidador(IUsuarioComandoValidador validadorUsuario)
        {
            _validadorUsuario = validadorUsuario;
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código não pode ser vazio.");
            
        }
    }
}
