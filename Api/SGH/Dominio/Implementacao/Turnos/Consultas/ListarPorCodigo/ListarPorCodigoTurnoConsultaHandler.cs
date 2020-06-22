using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPorCodigo
{
    public class ListarPorCodigoTurnoConsultaHandler : IRequestHandler<ListarPorCodigoTurnoConsulta, Turno>
    {
        private readonly ITurnoRepositorio _repositorio;

        public ListarPorCodigoTurnoConsultaHandler(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Turno> Handle(ListarPorCodigoTurnoConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.Consultar(lnq => lnq.Codigo == request.TurnoId);
        }
    }
}
