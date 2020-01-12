using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Usuarios.Consultas.ListarPaginacao
{
    public class ListarPaginacaoUsuarioConsulta : IRequest<Resposta<Paginacao<Usuario>>>
    {
        public Paginacao<Usuario> UsuarioPaginado { get; set; }
    }
}
