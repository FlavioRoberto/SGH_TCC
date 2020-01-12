using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.UsuarioPerfis.Comando.Remover
{
    public class RemoverUsuarioPerfilComandoValidador : AbstractValidator<RemoverUsuarioPerfilComando>, IRemoverUsuarioPerfilComandoValidador
    {
        private IUsuarioPerfilRepositorio _repositorio;

        public RemoverUsuarioPerfilComandoValidador(IUsuarioPerfilRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo de código não pode ser vazio.");
            RuleFor(lnq => lnq.Codigo).MustAsync(ValidarUsuarioPerfilExistente).WithMessage(comando => $"Não foi possível remover o perfil de código {comando.Codigo}.");
        }

        private async Task<bool> ValidarUsuarioPerfilExistente(int codigo, CancellationToken cancellationToken)
        {
            var perfil = await _repositorio.Listar(lnq => lnq.Codigo == codigo);

            if (perfil != null)
                return true;

            return false;
        }
    }
}
