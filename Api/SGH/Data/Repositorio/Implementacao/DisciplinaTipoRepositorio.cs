using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class DisciplinaTipoRepositorio : RepositorioBase<DisciplinaTipo>
    {

        public DisciplinaTipoRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public override async Task<Resposta<Paginacao<DisciplinaTipo>>> ListarPorPaginacao(Paginacao<DisciplinaTipo> entidadePaginada)
        {
            var query = GetDbSet().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<DisciplinaTipo>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            return await PaginacaoHelper<DisciplinaTipo>.Paginar(entidadePaginada, query);
        }

        protected override DbSet<DisciplinaTipo> GetDbSet()
        {
            return _contexto.DisciplinaTipo;
        }
    }
}
