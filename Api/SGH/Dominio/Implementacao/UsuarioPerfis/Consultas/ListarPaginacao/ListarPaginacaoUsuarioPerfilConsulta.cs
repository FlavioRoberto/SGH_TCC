using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.UsuarioPerfis.Consultas.ListarPaginacao
{
    public class ListarPaginacaoUsuarioPerfilConsulta: IRequest<Resposta<Paginacao<UsuarioPerfil>>>
    {
        public Paginacao<UsuarioPerfil> UsuarioPerfilPaginado { get; set; }
    }
}
