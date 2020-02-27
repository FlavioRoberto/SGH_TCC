using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoConsultaHandler : IRequestHandler<ListarPaginacaoTurnoConsulta, Paginacao<Turno>>
    {
        private readonly ITurnoRepositorio _repositorio;

        public ListarPaginacaoConsultaHandler(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<Turno>> Handle(ListarPaginacaoTurnoConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.TurnoPaginado);
        }
    }
}
