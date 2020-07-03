using MediatR;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarTodos
{
    public class ListarTodosTurnoConsulta : IRequest<ICollection<TurnoViewModel>>
    {
    }
}
