using FluentValidation;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar
{
    public class AtualizarSalaComandoValidador : SalaComandoValidador<AtualizarSalaComando>, IValidador<AtualizarSalaComando>
    {
        private readonly ISalaRepositorio _salaRepositorio;

        public AtualizarSalaComandoValidador(IBlocoRepositorio blocoRepositorio, ISalaRepositorio salaRepositorio) : base(blocoRepositorio)
        {
            _salaRepositorio = salaRepositorio;

            RuleFor(lnq => lnq.Codigo).NotNull().GreaterThan(0).WithMessage("O campo código é obrigatório.");

            When(lnq => lnq.Codigo > 0, () =>
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeSalaExiste).WithMessage(c => $"Não foi encontrada uma sala com o código {c.Codigo}.")
            );
        }

        private async Task<bool> ValidarSeSalaExiste(int codigoSala, CancellationToken arg2)
        {
            return await _salaRepositorio.Contem(lnq => lnq.Codigo == codigoSala);
        }
    }
}
