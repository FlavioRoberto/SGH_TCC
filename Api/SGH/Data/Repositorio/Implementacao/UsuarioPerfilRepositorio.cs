using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
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
