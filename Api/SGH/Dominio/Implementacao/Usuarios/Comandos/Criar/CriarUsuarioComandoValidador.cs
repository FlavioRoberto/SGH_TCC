using FluentValidation;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoValidador : AbstractValidator<IUsuarioComando>, ICriarUsuarioComandoValidador
    {
        private IUsuarioComandoValidador _validadorUsuario;

        public CriarUsuarioComandoValidador(IUsuarioComandoValidador validadorUsuario)
        {
            _validadorUsuario = validadorUsuario;

            RuleFor(lnq => lnq).MustAsync(_validadorUsuario.ValidarUsuarioComMesmoLogin).WithMessage("O login informado já esta em uso.");
            RuleFor(lnq => lnq).MustAsync(_validadorUsuario.ValidarUsuarioComMesmoEmail).WithMessage("O e-mail informado já esta em uso.");

        }

    }
}
