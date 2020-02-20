using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Curriculos.Consultas.ListarTodos
{
    public class ListarTodosCurriculosConsulta : IRequest<Resposta<List<CurriculoViewModel>>>
    {
    }
}
