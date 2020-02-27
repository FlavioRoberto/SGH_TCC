using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarTodos
{
    public class ListarTodosTurnoComandoHandler : IRequestHandler<ListarTodosTurnoConsulta, ICollection<Turno>>
    {

        private readonly ITurnoRepositorio _repositorio;

        public ListarTodosTurnoComandoHandler(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ICollection<Turno>> Handle(ListarTodosTurnoConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarTodos();    
        }
    }
}
