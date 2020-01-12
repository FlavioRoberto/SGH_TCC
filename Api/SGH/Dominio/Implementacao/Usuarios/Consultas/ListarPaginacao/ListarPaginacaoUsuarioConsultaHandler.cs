using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Consultas.ListarPaginacao
{
    public class ListarPaginacaoUsuarioConsultaHandler : IRequestHandler<ListarPaginacaoUsuarioConsulta, Resposta<Paginacao<Usuario>>>
    {
        private IUsuarioRepositorio _repositorio;

        public ListarPaginacaoUsuarioConsultaHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Paginacao<Usuario>>> Handle(ListarPaginacaoUsuarioConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPorPaginacao(request.UsuarioPaginado);
            return resultado;
        }
    }
}
