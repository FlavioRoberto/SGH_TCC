using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Consultas.ListarPaginacao
{
    public class ListarPaginacaoDisciplinaConsultaHandler : IRequestHandler<ListarPaginacaoDisciplinaConsulta, Paginacao<Disciplina>>
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public ListarPaginacaoDisciplinaConsultaHandler(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<Disciplina>> Handle(ListarPaginacaoDisciplinaConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.DisciplinaPaginacao);
        }
    }
}
