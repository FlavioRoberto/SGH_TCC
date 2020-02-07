using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComandoValidador : AbstractValidator<AtualizarUsuarioComando>, IAtualizarUsuarioComandoValidador
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AtualizarUsuarioComandoValidador(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            RuleFor(lnq => lnq.Codigo).GreaterThan(0).WithMessage("O campo código não pode ser vazio.");
            When(lnq => lnq.Codigo.HasValue && lnq.Codigo.Value > 0, () =>
            {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarUsuarioExistente).WithMessage(c => $"Não foi encontrado um usuário com o código {c.Codigo}.");
            });
        }

        private Task<bool> ValidarUsuarioExistente(int? codigo, CancellationToken cancellationToken)
        {
            return _usuarioRepositorio.Contem(lnq => lnq.Codigo == codigo.Value);
        }
    }
}
