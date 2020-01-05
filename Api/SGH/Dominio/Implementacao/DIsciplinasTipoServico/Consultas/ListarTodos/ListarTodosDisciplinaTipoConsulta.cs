using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Consultas.ListarTodos
{
    public class ListarTodosDisciplinaTipoConsulta : IRequest<Resposta<List<DisciplinaTipo>>>
    {
    }
}
