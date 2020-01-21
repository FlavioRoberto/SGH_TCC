using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Consultas.ListarPaginacao
{
    public class ListarPaginacaoDisciplinaTipoConsultaHandler : IRequestHandler<ListarPaginacaoDisciplinaTipoConsulta, Paginacao<DisciplinaTipo>>
    {

        private readonly IDisciplinaTipoRepositorio _repositorio;

        public ListarPaginacaoDisciplinaTipoConsultaHandler(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<DisciplinaTipo>> Handle(ListarPaginacaoDisciplinaTipoConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.DisciplinaTipoPaginacao);
        }
    }
}
