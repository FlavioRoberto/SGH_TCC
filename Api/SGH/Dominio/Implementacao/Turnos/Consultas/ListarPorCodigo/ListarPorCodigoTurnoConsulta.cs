using MediatR;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Turnos.Consultas.ListarPorCodigo
{
    public class ListarPorCodigoTurnoConsulta : IRequest<Turno>
    {
        public int TurnoId { get; set; }
    }
}
