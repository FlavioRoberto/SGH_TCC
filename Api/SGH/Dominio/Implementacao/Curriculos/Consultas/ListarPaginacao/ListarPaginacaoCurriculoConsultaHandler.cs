using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Curriculos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCurriculoConsultaHandler : IRequestHandler<ListarPaginacaoCurriculoConsulta, Paginacao<Curriculo>>
    {
        private ICurriculoRepositorio _repositorio;

        public ListarPaginacaoCurriculoConsultaHandler(ICurriculoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<Curriculo>> Handle(ListarPaginacaoCurriculoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPorPaginacao(request.CurriculoPaginado);
            return resultado;
        }
    }
}