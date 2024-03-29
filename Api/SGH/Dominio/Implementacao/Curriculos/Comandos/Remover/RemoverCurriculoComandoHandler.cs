﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover
{
    public class RemoverCurriculoComandoHandler : IRequestHandler<RemoverCurriculoComando, Resposta<bool>>
    {
        private readonly ICurriculoRepositorio _repositorio;
        private readonly IValidador<RemoverCurriculoComando> _validador;

        public RemoverCurriculoComandoHandler(ICurriculoRepositorio repositorio, IValidador<RemoverCurriculoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverCurriculoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(Convert.ToInt32(request.CodigoCurriculo));

            return new Resposta<bool>(resultado);
        }
    }
}
