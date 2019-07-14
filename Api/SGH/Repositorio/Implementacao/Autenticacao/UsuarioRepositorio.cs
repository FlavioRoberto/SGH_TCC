using Data.Contexto;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Implementacao.Autenticacao
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>
    {
        public UsuarioRepositorio(MySqlContext contexto) : base(contexto)
        { }

        public override async Task<Resposta<Paginacao<Usuario>>> ListarPorPaginacao(Paginacao<Usuario> entidadePaginada)
        {
            var query = GetDbSet().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new Usuario();

            var entidade = entidadePaginada.Entidade;

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Email))
                query = query.Where(lnq => lnq.Email.Contains(entidade.Email));

            if (!string.IsNullOrEmpty(entidade.Login))
                query = query.Where(lnq => lnq.Login.Contains(entidade.Login));

            if (!string.IsNullOrEmpty(entidade.Nome))
                query = query.Where(lnq => lnq.Nome.Contains(entidade.Nome));

            if (!string.IsNullOrEmpty(entidade.Telefone))
                query = query.Where(lnq => lnq.Telefone.Contains(entidade.Telefone));

            if (entidade.PerfilCodigo > 0)
                query = query.Where(lnq => lnq.PerfilCodigo == entidade.PerfilCodigo);
            
            return await PaginacaoHelper<Usuario>.Paginar(entidadePaginada, query);
        }

        protected override DbSet<Usuario> GetDbSet()
        {
            return _contexto.Usuario;
        }
    }
}
