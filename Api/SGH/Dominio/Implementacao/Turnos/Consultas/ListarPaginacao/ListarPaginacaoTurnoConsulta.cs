using MediatR;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoTurnoConsulta : IRequest<Paginacao<TurnoViewModel>>
    {
        public Paginacao<Turno> TurnoPaginado { get; set; }
    }
}
