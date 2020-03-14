﻿using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComandoHandler : IRequestHandler<AtualizarTurnoComando, Resposta<Turno>>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly IValidador<AtualizarTurnoComando> _validador;

        public AtualizarTurnoComandoHandler(ITurnoRepositorio repositorio, IValidador<AtualizarTurnoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Turno>> Handle(AtualizarTurnoComando request, CancellationToken cancellationToken)
        {

            var erros = _validador.Validar(request);

            if (string.IsNullOrEmpty(erros))
                return new Resposta<Turno>(erros);

            var turno = new Turno
            {
                Codigo = request.TurnoId,
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Atualizar(turno);
            return new Resposta<Turno>(resultado);
        }
    }
}
