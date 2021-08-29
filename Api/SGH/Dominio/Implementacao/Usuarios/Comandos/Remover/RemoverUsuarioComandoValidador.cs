using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Remover
{
    public class RemoverUsuarioComandoValidador : Validador<RemoverUsuarioComando>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public RemoverUsuarioComandoValidador(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.CodigoUsuario).NotEmpty().WithMessage("O código do usuário não pode ser vazio.");
            RuleFor(lnq => lnq.CodigoUsuario).MustAsync(ValidarUsuarioExistente).WithMessage(codigo => $"O usuário de código {codigo} não foi encontrado.");
            RuleFor(lnq => lnq.CodigoUsuario).MustAsync(ValidarUltimoUsuarioAdministrador).WithMessage("Não é possível remover o usuário, pois não existem outros usuários administradores!");
        }

        private async Task<bool> ValidarUltimoUsuarioAdministrador(int codigoUsuario, CancellationToken cancellationToken)
        {
            var quantidadeUsuariosAdm = await(_repositorio as IUsuarioRepositorio).QuantidadeUsuarioAdm();
            var usuarioARemover = await _repositorio.Consultar(lnq => lnq.Codigo == codigoUsuario);
            var usuarioAdm = usuarioARemover.Perfil.Administrador == true;

            if (usuarioAdm && quantidadeUsuariosAdm <= 1)
                return false;

            return true;
        }

        private async Task<bool> ValidarUsuarioExistente(int codigoUsuario, CancellationToken cancellationToken)
        {
            var usuario = await _repositorio.Consultar(lnq => lnq.Codigo == codigoUsuario);
            
            if (usuario == null)
                return false;

            return true;
        }
    }
}
