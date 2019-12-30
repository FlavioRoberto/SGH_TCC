using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarPaginacao
{
    public class ListarPaginacaoDisciplinaConsultaHandler : IRequestHandler<ListarPaginacaoDisciplinaConsulta, Resposta<Paginacao<Disciplina>>>
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public ListarPaginacaoDisciplinaConsultaHandler(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Paginacao<Disciplina>>> Handle(ListarPaginacaoDisciplinaConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPorPaginacao(request.DisciplinaPaginacao);
            return resultado;
        }
    }
}
