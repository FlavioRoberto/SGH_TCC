using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Cursos.Consultas.ListarTodos
{
    public class ListarTodosCursosConsulta : IRequest<List<Curso>>
    {
    }
}
