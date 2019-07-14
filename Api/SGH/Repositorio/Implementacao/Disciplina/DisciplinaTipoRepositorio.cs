using Data.Contexto;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaTipoRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.DisciplinaTipo>
    {

        public DisciplinaTipoRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override async Task<Resposta<Paginacao<DisciplinaTipo>>> ListarPorPaginacao(Paginacao<DisciplinaTipo> entidadePaginada)
        {
            var query = GetDbSet().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new DisciplinaTipo();

            var entidade = entidadePaginada.Entidade;

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
