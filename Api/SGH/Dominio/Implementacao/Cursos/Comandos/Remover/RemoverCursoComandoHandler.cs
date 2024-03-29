﻿using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Remover
{
    public class RemoverCursoComandoHandler : IRequestHandler<RemoverCursoComando, Resposta<bool>>
    {
        private readonly ICursoRepositorio _repositorio;
        private readonly IValidador<RemoverCursoComando> _validador;

        public RemoverCursoComandoHandler(ICursoRepositorio repositorio, IValidador<RemoverCursoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverCursoComando request, CancellationToken cancellationToken)
        {
            string erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.CursoId);

            return new Resposta<bool>(resultado);
        }
    }
}
