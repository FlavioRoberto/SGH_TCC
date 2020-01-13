using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cursos.Consultas.ListarTodos
{
    public class ListarTodosCursosConsultaHandler : IRequestHandler<ListarTodosCursosConsulta, Resposta<List<Curso>>>
    {
        private readonly ICursoRepositorio _repositorio;

        public ListarTodosCursosConsultaHandler(ICursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<List<Curso>>> Handle(ListarTodosCursosConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<Curso>>(resultado);
        }
    }
}
