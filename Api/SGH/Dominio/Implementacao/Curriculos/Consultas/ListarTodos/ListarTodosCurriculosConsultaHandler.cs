using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarTodos
{
    public class ListarTodosCurriculosConsultaHandler : IRequestHandler<ListarTodosCurriculosConsulta, Resposta<List<CurriculoViewModel>>>
    {
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly IMapper _mapper;

        public ListarTodosCurriculosConsultaHandler(ICurriculoRepositorio curriculoRepositorio, IMapper mapper)
        {
            _curriculoRepositorio = curriculoRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<List<CurriculoViewModel>>> Handle(ListarTodosCurriculosConsulta request, CancellationToken cancellationToken)
        {
            var curriculos = await _curriculoRepositorio.ListarTodos();

            var retorno = _mapper.Map<List<CurriculoViewModel>>(curriculos);

            return new Resposta<List<CurriculoViewModel>>(retorno);
        }
    }
}