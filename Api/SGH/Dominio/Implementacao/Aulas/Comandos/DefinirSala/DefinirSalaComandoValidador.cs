using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Contratos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.DefinirSala
{
    public class DefinirSalaComandoValidador : Validador<DefinirSalaComando>
    {
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ISalaRepositorio _salaRepositorio;
        private bool salaEncontrada = false;
        private bool aulaEncontrada = false;

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

            When(lnq => salaEncontrada && aulaEncontrada, () =>
            {
                RuleFor(lnq => lnq)
                  .MustAsync(ValidarSeSalaDisponivel)
                  .WithMessage("A sala selecionada não esta disponível nesse dia e horário.");
            });              

        }

        private async Task<bool> ValidarSeSalaDisponivel(DefinirSalaComando comando, CancellationToken arg2)
        {
            var aula = await _aulaRepositorio.Consultar(comando.AulaId);

            var reserva = aula.Reserva;

            var aulaReservada = await _aulaRepositorio.Contem(lnq => lnq.CodigoSala == comando.SalaId &&
                                                                     lnq.Reserva.Hora == reserva.Hora &&
                                                                     lnq.Reserva.DiaSemana == reserva.DiaSemana);
            if (aulaReservada)
                return false;

            return true;
        }

        private async Task<bool> ValidarSeSalaExiste(long salaId, CancellationToken cancellationToken)
        {
            salaEncontrada = await _salaRepositorio.Contem(lnq => lnq.Codigo == salaId);
            return salaEncontrada;
        }

        private async Task<bool> ValidarSeAulaExiste(long aulaId, CancellationToken cancellationToken)
        {
            aulaEncontrada = await _aulaRepositorio.Contem(lnq => lnq.Codigo == aulaId);
            return aulaEncontrada;
        }
    }
}
