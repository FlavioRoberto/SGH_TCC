using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Salas.Consultas.ListarTodas
{
    public class ListarTodasSalasConsulta : IRequest<Resposta<ICollection<SalaViewModel>>>
    {
    }
}
