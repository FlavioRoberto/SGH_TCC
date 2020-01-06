using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComandoValidador : AbstractValidator<AtualizarUsuarioComando>, IAtualizarUsuarioComandoValidador
    {
        private IUsuarioRepositorio _repositorio;

        public AtualizarUsuarioComandoValidador(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código não pode ser vazio.");
            RuleFor(lnq => lnq).MustAsync(ValidarUsuarioComMesmoLogin).WithMessage("O login informado já esta em uso.");
            RuleFor(lnq => lnq).MustAsync(ValidarUsuarioComMesmoEmail).WithMessage("O e-mail informado já esta em uso.");
        }

        private async Task<bool> ValidarUsuarioComMesmoEmail(AtualizarUsuarioComando comando, CancellationToken cancellationToken)
        {
            var msmEmail = await _repositorio
                                .Listar(lnq => lnq.Email.IgualA(comando.Email)
                                 && lnq.Codigo != comando.Codigo) != null;

            if (msmEmail)
                return false;

            return true;
        }

        private async Task<bool> ValidarUsuarioComMesmoLogin(AtualizarUsuarioComando comando, CancellationToken cancellationToken)
        {
            var msmLogin = await _repositorio.Listar(lnq => lnq.Login.IgualA(comando.Login)
                                 && comando.Codigo != lnq.Codigo) != null;

            if (msmLogin)
                return false;

            return true;
        }
    }
}
