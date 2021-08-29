using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Repositories;
using SHG.Data.Contexto;

namespace SGH.Data.Repositorio.Implementacao
{
    public class UsuarioPerfilRepositorio : RepositorioBase<UsuarioPerfil>, IUsuarioPerfilRepositorio
    {
        public UsuarioPerfilRepositorio(IContexto contexto) : base(contexto)
        {
        }
    }
}
