using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.UsuarioPerfis.Consultas.ListarPaginacao
{
    public class ListarPaginacaoUsuarioPefilConsultaHandler : IRequestHandler<ListarPaginacaoUsuarioPerfilConsulta, Paginacao<UsuarioPerfil>>
    {
        private readonly IUsuarioPerfilRepositorio _repositorio;

        public ListarPaginacaoUsuarioPefilConsultaHandler(IUsuarioPerfilRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Paginacao<UsuarioPerfil>> Handle(ListarPaginacaoUsuarioPerfilConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.ListarPorPaginacao(request.UsuarioPerfilPaginado);
        }
    }
}
