using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Consultas.ListarTodas
{
    public class ListarTodasSalasConsultaHandler : IRequestHandler<ListarTodasSalasConsulta, Resposta<ICollection<SalaViewModel>>>
    {
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly IMapper _mapper;

        public ListarTodasSalasConsultaHandler(ISalaRepositorio salaRepositorio, IMapper mapper)
        {
            _salaRepositorio = salaRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<ICollection<SalaViewModel>>> Handle(ListarTodasSalasConsulta request, CancellationToken cancellationToken)
        {
            var salas = await _salaRepositorio.ListarTodos();
            var salasViewModel = _mapper.Map<List<SalaViewModel>>(salas);
            return new Resposta<ICollection<SalaViewModel>>(salasViewModel);
        }
    }
}
