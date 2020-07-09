using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Shared.Extensions;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.AtualizarSenha
{
    public class AtualizarSenhaComandoValidador : AbstractValidator<AtualizarSenhaComando>, IValidador<AtualizarSenhaComando>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IUsuarioResolverService _usuarioResolverService;
        private Usuario _usuario;

        public AtualizarSenhaComandoValidador(IUsuarioRepositorio  repositorio, IUsuarioResolverService usuarioResolverService)
        {
            _repositorio = repositorio;
            _usuarioResolverService = usuarioResolverService;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.Senha).NotEmpty().WithMessage("O campo de senha não pode ser vazio.");
            RuleFor(lnq => lnq.NovaSenha).NotEmpty().WithMessage("O campo de nova senha não pode ser vazio.");
            RuleFor(lnq => lnq).MustAsync(ValidarUsuarioNaoEncontrado).WithMessage("Usuário não encontrado!");
            When(lnq => _usuario != null, () => {
                RuleFor(lnq => lnq).Must(ValidarSenhaIncorreta).WithMessage("Senha incorreta!");
            });
        }

        private bool ValidarSenhaIncorreta(AtualizarSenhaComando comando)
        {
            if (!_usuario.Senha.Equals(comando.Senha.ToMD5()))
                return false;

            return true;
        }

        private async Task<bool> ValidarUsuarioNaoEncontrado(AtualizarSenhaComando comando, CancellationToken cancellationToken)
        {
            var codigoUsuarioLogado = _usuarioResolverService.RetornarCodigoUsuario();
            _usuario = await _repositorio.Consultar(lnq => lnq.Codigo == codigoUsuarioLogado);

            if (_usuario == null)
                return false;

            return true;
        }
    }
}
