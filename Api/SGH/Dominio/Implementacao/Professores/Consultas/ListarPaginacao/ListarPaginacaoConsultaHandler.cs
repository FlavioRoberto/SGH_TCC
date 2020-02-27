using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Consultas.ListarPaginacao
{
    public class ListarPaginacaoConsultaHandler : IRequestHandler<ListarPaginacaoProfessorConsulta, Paginacao<Professor>>
    {
        private readonly IProfessorRepositorio _repositorio;

        public ListarPaginacaoConsultaHandler(IProfessorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<Professor>> Handle(ListarPaginacaoProfessorConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.ProfessorPaginado);
        }
    }
}
