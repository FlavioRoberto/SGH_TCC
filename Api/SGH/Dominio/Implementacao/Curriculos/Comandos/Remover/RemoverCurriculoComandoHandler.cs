﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Remover
{
    public class RemoverCurriculoComandoHandler : IRequestHandler<RemoverCurriculoComando, Resposta<bool>>
    {
        private readonly ICurriculoRepositorio _repositorio;
        private readonly IRemoverCurriculoComandoValidador _validador;

        public RemoverCurriculoComandoHandler(ICurriculoRepositorio repositorio, IRemoverCurriculoComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverCurriculoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.CodigoCurriculo);

            return new Resposta<bool>(resultado);
        }
    }
}