using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Turnos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoConsultaHandler : IRequestHandler<ListarPaginacaoConsulta, Paginacao<Turno>>
    {
        private readonly ITurnoRepositorio _repositorio;

        public ListarPaginacaoConsultaHandler(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<Turno>> Handle(ListarPaginacaoConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.TurnoPaginado);
        }
    }
}
