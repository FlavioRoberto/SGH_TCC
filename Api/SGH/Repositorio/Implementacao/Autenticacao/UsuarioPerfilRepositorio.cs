using Data.Contexto;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Autenticacao
{
    public class UsuarioPerfilRepositorio : RepositorioBase<UsuarioPerfil>
    {
        public UsuarioPerfilRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override Resposta<Paginacao<UsuarioPerfil>> ListarPorPaginacao(Paginacao<UsuarioPerfil> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<UsuarioPerfil> GetDbSet()
        {
            return _contexto.UsuarioPerfil;
        }
    }
}
