using System;
using System.Linq;
using System.Threading.Tasks;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Helpers
{
    public class PaginacaoHelper<T>
    {
        public static async Task<Resposta<Paginacao<T>>> Paginar(Paginacao<T> entidadePaginada, IQueryable<T> query)
        {
            var total = await query.CountAsync();
            var queryPaginada = query.AsEnumerable()
                .Select((lnq, i) => new Paginacao<T>
                {
                    Entidade = lnq,
                    Posicao = i + 1
                })
                .Skip(entidadePaginada.Posicao - 1)
                .Take(1);
            
            entidadePaginada = queryPaginada.FirstOrDefault();

            if (entidadePaginada == null)
                return new Resposta<Paginacao<T>>(null, "Não foram encontrados dados!");

            if (entidadePaginada.Entidade == null)
                return new Resposta<Paginacao<T>>(null, "Não foram encontrados dados!");

            entidadePaginada.Total = total;

            return new Resposta<Paginacao<T>>(entidadePaginada);
        }
    }
}
