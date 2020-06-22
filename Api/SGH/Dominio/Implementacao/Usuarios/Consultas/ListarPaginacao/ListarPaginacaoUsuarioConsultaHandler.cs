using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Consultas.ListarPaginacao
{
    public class ListarPaginacaoUsuarioConsultaHandler : IRequestHandler<ListarPaginacaoUsuarioConsulta, Paginacao<Usuario>>
    {
        private IUsuarioRepositorio _repositorio;

        public ListarPaginacaoUsuarioConsultaHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<Usuario>> Handle(ListarPaginacaoUsuarioConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.UsuarioPaginado);
        }
    }
}
