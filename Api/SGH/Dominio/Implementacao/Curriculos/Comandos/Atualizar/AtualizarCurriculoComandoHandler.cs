﻿using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Core.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Atualizar
{
    public class AtualizarCurriculoComandoHandler : IRequestHandler<AtualizarCurriculoComando, Resposta<Curriculo>>
    {
        private ICurriculoRepositorio _repositorio;
        private IMapper _mapper;
        private IValidador<AtualizarCurriculoComando> _validador;

        public AtualizarCurriculoComandoHandler(ICurriculoRepositorio curriculoRepositorio, IMapper mapper, IValidador<AtualizarCurriculoComando> validador)
        {
            _repositorio = curriculoRepositorio;
            _mapper = mapper;
            _validador = validador;
        }

        public async Task<Resposta<Curriculo>> Handle(AtualizarCurriculoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Curriculo>(erros);

            var entidade = _mapper.Map<Curriculo>(request);

            var curriculo = await _repositorio.Atualizar(entidade);

            if (curriculo.Disciplinas != null && curriculo.Disciplinas.Any())
                curriculo.Disciplinas = curriculo.Disciplinas.OrderBy(lnq => lnq.Periodo).ToList();

            return new Resposta<Curriculo>(curriculo);
        }
    }
}
