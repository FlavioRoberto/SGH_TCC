using MediatR;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPorCodigo
{
    public class ListarPorCodigoTurnoConsulta : IRequest<TurnoViewModel>
    {
        public int TurnoId { get; set; }
    }
}
