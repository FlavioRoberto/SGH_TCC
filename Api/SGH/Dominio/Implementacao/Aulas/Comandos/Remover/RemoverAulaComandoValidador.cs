using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Remover
{
    public class RemoverAulaComandoValidador : Validador<RemoverAulaComando>
    {
        private readonly IAulaRepositorio _aulaRepositorio;

        public RemoverAulaComandoValidador(IAulaRepositorio aulaRepositorio)
        {
            _aulaRepositorio = aulaRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.CodigoAula)
                .NotEmpty()
                .WithMessage("O código da aula não foi informado.")

                .MustAsync(ValidarSeAulaExiste)
                .WithMessage(c => $"Não foi encontrada uma aula com o código {c.CodigoAula}.");
        }

        private async Task<bool> ValidarSeAulaExiste(int codigoAula, CancellationToken arg2)
        {
            return await _aulaRepositorio.Contem(lnq => lnq.Codigo == codigoAula);
        }
    }
}
