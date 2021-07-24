using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Data.Repositorio.Helpers
{
    public class PaginacaoHelper<T>
    {
        public static async Task<Paginacao<T>> Paginar(Paginacao<T> entidadePaginada, IQueryable<T> query)
        {
            var total = await query.CountAsync();
            var queryPaginada = query.AsEnumerable()
                                     .Select((n, i)=> new {
                                         Value = n,
                                         Index = i
                                     })
                                     .Skip(entidadePaginada.Posicao - 1)
                                     .Take(entidadePaginada.Quantidade)
                                     .ToList();

            if (queryPaginada.Count <= 0)
                return new Paginacao<T>();

            entidadePaginada.Entidade = queryPaginada.Select(lnq => lnq.Value).ToList();
            entidadePaginada.Posicao = queryPaginada.LastOrDefault().Index + 1;
            entidadePaginada.Total = total;

            return entidadePaginada;
        }
    }
}
