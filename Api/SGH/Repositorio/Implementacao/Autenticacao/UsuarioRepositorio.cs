using Data.Contexto;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Global;
using Global.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contratos;
using Repositorio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositorio.Implementacao.Autenticacao
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(MySqlContext contexto) : base(contexto)
        { }

        public override async Task<Resposta<Paginacao<Usuario>>> ListarPorPaginacao(Paginacao<Usuario> entidadePaginada)
        {
            var query = GetDbSet()
                .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Usuario>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

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

        public async Task<int> QuantidadeUsuarioAdm()
        {
            return await GetDbSet()
                 .Include(lnq => lnq.Perfil)
                 .AsNoTracking()
                 .CountAsync(lnq => lnq.Perfil.Administrador == true);
        }

        public async Task<Usuario> RetornarUsuarioPorLoginESenha(string login, string senha)
        {
            return await GetDbSet()
                .Include(lnq => lnq.Perfil)
                .AsNoTracking()
                .FirstOrDefaultAsync(lnq =>
                    lnq.Login.IgualA(login)
                    && lnq.Senha.IgualA(senha.ToMD5()));

        }

        public override  async Task<Usuario> Listar(Expression<Func<Usuario, bool>> query)
        {
            return await GetDbSet()
                    .AsNoTracking()
                    .Include(lnq => lnq.Perfil)
                    .AsQueryable()
                    .FirstOrDefaultAsync(query);
        }

        protected override DbSet<Usuario> GetDbSet()
        {
            return _contexto.Usuario;
        }
    }
}
