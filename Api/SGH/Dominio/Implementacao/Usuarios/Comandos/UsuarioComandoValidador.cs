using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos
{
    public abstract class UsuarioComandoValidador<T> : AbstractValidator<T> where T : IUsuarioComando
    {
        protected readonly IUsuarioRepositorio _repositorio;

        public UsuarioComandoValidador(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.Email).NotEmpty().WithMessage("O campo de e-mail não pode ser vazio.");
            RuleFor(lnq => lnq.Login).NotEmpty().WithMessage("O campo de login não pode ser vazio.");
            RuleFor(lnq => lnq.Nome).NotEmpty().WithMessage("O campo de nome não pode ser vazio.");
            RuleFor(lnq => lnq.PerfilCodigo).NotEmpty().WithMessage("O campo de perfil não pode ser vazio.");
            RuleFor(lnq => lnq).MustAsync(ValidarUsuarioComMesmoEmail).WithMessage(comando => $"Já existe um usuário cadastrado com o e-mail {comando.Email}");
            RuleFor(lnq => lnq).MustAsync(ValidarUsuarioComMesmoLogin).WithMessage(comando => $"Já existe um usuário cadastrado com o login {comando.Login}");
        }

        protected async Task<bool> ValidarUsuarioComMesmoEmail(T comando, CancellationToken cancellationToken)
        {
            var msmEmail = await _repositorio
                            .Contem(lnq => lnq.Email.IgualA(comando.Email));

            if (comando.Codigo.HasValue)
                msmEmail = await _repositorio
                                .Contem(lnq => lnq.Email.IgualA(comando.Email)
                                 && lnq.Codigo != comando.Codigo);

            if (msmEmail)
                return false;

            return true;
        }

        protected async Task<bool> ValidarUsuarioComMesmoLogin(T comando, CancellationToken cancellationToken)
        {
            var msmLogin = await _repositorio.Consultar(lnq => lnq.Login.IgualA(comando.Login)
                                 && comando.Codigo != lnq.Codigo) != null;

            if (msmLogin)
                return false;

            return true;
        }

    }
}
