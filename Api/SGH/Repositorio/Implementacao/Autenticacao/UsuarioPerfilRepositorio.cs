using Data.Contexto;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repositorio.Implementacao.Autenticacao
{
    public class UsuarioPerfilRepositorio : RepositorioBase<UsuarioPerfil>
    {
        public UsuarioPerfilRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override Task<Resposta<Paginacao<UsuarioPerfil>>> ListarPorPaginacao(Paginacao<UsuarioPerfil> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<UsuarioPerfil> GetDbSet()
        {
            return _contexto.UsuarioPerfil;
        }
    }
}
