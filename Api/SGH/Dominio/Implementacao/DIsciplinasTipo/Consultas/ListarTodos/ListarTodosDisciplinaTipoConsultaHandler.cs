using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Consultas.ListarTodos
{
    public class ListarTodosDisciplinaTipoConsultaHandler : IRequestHandler<ListarTodosDisciplinaTipoConsulta, List<DisciplinaTipo>>
    {

        private readonly IDisciplinaTipoRepositorio _repositorio;

        public ListarTodosDisciplinaTipoConsultaHandler(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<DisciplinaTipo>> Handle(ListarTodosDisciplinaTipoConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarTodos();
        }
    }
}
