using MediatR;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Blocos.Consultas.ListarTodos
{
    public class ListarTodosBlocoConsulta : IRequest<List<BlocoViewModel>>
    {
    }
}
