using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Consultas.ListarTodos
{
    public class ListarTodosDisciplinaTipoConsulta : IRequest<List<DisciplinaTipo>>
    {
    }
}
