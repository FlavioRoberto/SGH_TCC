using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cursos.Consultas.ListarTodos
{
    public class ListarTodosCursosConsultaHandler : IRequestHandler<ListarTodosCursosConsulta, List<Curso>>
    {
        private readonly ICursoRepositorio _repositorio;

        public ListarTodosCursosConsultaHandler(ICursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Curso>> Handle(ListarTodosCursosConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarTodos();
            return resultado;
        }
    }
}
