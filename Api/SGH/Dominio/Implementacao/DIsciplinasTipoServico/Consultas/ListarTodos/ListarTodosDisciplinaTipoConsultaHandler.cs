using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Consultas.ListarTodos
{
    public class ListarTodosDisciplinaTipoConsultaHandler : IRequestHandler<ListarTodosDisciplinaTipoConsulta, Resposta<List<DisciplinaTipo>>>
    {

        private readonly IDisciplinaTipoRepositorio _repositorio;

        public ListarTodosDisciplinaTipoConsultaHandler(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<List<DisciplinaTipo>>> Handle(ListarTodosDisciplinaTipoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<DisciplinaTipo>>(resultado);
        }
    }
}
