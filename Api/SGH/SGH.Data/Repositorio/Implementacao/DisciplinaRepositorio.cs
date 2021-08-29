using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Repositories;
using SHG.Data.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class DisciplinaRepositorio : RepositorioBase<Disciplina>, IDisciplinaRepositorio
    {

        public DisciplinaRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public async Task<Paginacao<Disciplina>> ListarPorPaginacao(Paginacao<Disciplina> entidadePaginada)
        {
            var query = GetDbSet<Disciplina>().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Disciplina>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));


            return await PaginacaoHelper<Disciplina>.Paginar(entidadePaginada, query);
        }
    }
}
