using MediatR;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Turnos.Consultas.ListarTodos
{
    public class ListarTodosTurnoConsulta : IRequest<ICollection<Turno>>
    {
    }
}
