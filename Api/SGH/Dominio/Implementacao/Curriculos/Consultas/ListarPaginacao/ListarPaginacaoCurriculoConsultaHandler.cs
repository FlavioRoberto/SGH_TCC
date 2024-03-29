﻿using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCurriculoConsultaHandler : IRequestHandler<ListarPaginacaoCurriculoConsulta, Paginacao<CurriculoViewModel>>
    {
        private ICurriculoRepositorio _repositorio;
        private IMapper _mapper;

        public ListarPaginacaoCurriculoConsultaHandler(ICurriculoRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Paginacao<CurriculoViewModel>> Handle(ListarPaginacaoCurriculoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPorPaginacao(request.CurriculoPaginado);
            var retorno = _mapper.Map<Paginacao<CurriculoViewModel>>(resultado);
            return retorno;
        }
    }
}