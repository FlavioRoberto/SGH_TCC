using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.DefinirSala
{
    public class DefinirSalaComandoValidador : Validador<DefinirSalaComando>
    {
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ISalaRepositorio _salaRepositorio;

        public DefinirSalaComandoValidador(IAulaRepositorio aulaRepositorio,
                                           ISalaRepositorio salaRepositorio)
        {
            _aulaRepositorio = aulaRepositorio;
            _salaRepositorio = salaRepositorio;

            RuleFor(lnq => lnq.AulaId)
                .NotEmpty()
                .WithMessage("O campo aulaId não foi informado.")
                .MustAsync(ValidarSeAulaExiste)
                .WithMessage(x => $"Não foi encontrada uma aula com o id {x.AulaId}.");

            RuleFor(lnq => lnq.SalaId)
                .NotEmpty()
                .WithMessage("O campo salaId não foi informado.")
                .MustAsync(ValidarSeSalaExiste)
                .WithMessage(x => $"Não foi encontrada uma sala com o id {x.SalaId}.");

        }

        private async Task<bool> ValidarSeSalaExiste(long salaId, CancellationToken cancellationToken)
        {
            return await _salaRepositorio.Contem(lnq => lnq.Codigo == salaId);
        }

        private async Task<bool> ValidarSeAulaExiste(long aulaId, CancellationToken cancellationToken)
        {
            return await _aulaRepositorio.Contem(lnq => lnq.Codigo == aulaId);
        }
    }
}
