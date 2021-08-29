using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Remover
{
    public class RemoverCargoDisciplinaComandoValidador : Validador<RemoverCargoDisciplinaComando>
    {
        private readonly ICargoDisciplinaRepositorio _repositorio;

        public RemoverCargoDisciplinaComandoValidador(ICargoDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.Codigo).GreaterThan(0).WithMessage("O campo código não pode ser menor ou igual a 0.");

            When(lnq => lnq.Codigo > 0, () =>
            {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeCargoDisciplinaComandoExiste).WithMessage(c => $"Não foi encontrada uma disciplina com o código {c.Codigo}.");
            });
        }

        private async Task<bool> ValidarSeCargoDisciplinaComandoExiste(int codigo, CancellationToken arg2)
        {
            return await _repositorio.Contem(lnq => lnq.Codigo == codigo);
        }
    }
}
