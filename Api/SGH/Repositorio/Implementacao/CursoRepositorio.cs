using Data.Contexto;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;
using System.Linq;

namespace Repositorio.Implementacao
{
    public class CursoRepositorio : RepositorioBase<Curso>
    {

        public CursoRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override Resposta<Paginacao<Curso>> ListarPorPaginacao(Paginacao<Curso> entidadePaginada)
        {
            var query = _contexto.Curso.AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new Curso();

            var entidade = entidadePaginada.Entidade;

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            return PaginacaoHelper<Curso>.Paginar(entidadePaginada, query);
        }

        protected override DbSet<Curso> GetDbSet()
        {
            return _contexto.Curso;
        }
    }
}
