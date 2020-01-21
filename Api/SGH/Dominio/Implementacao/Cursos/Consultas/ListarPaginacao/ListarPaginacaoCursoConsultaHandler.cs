using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cursos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCursoConsultaHandler : IRequestHandler<ListarPaginacaoCursoConsulta, Paginacao<Curso>>
    {
        private readonly ICursoRepositorio _repositorio;

        public ListarPaginacaoCursoConsultaHandler(ICursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<Paginacao<Curso>> Handle(ListarPaginacaoCursoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = _repositorio.ListarPorPaginacao(request.CursoPaginado);
            return resultado;
        }
    }
}
