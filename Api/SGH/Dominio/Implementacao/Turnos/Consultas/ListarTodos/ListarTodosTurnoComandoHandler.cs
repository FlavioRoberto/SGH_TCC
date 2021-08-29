using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarTodos
{
    public class ListarTodosTurnoComandoHandler : IRequestHandler<ListarTodosTurnoConsulta, ICollection<TurnoViewModel>>
    {

        private readonly ITurnoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ListarTodosTurnoComandoHandler(ITurnoRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<ICollection<TurnoViewModel>> Handle(ListarTodosTurnoConsulta request, CancellationToken cancellationToken)
        {
            var resultado =  await _repositorio.ListarTodos();
            return _mapper.Map<List<TurnoViewModel>>(resultado);
        }
    }
}
