using System;
using System.Linq;
using Dominio.ViewModel;
using Global;

namespace Repositorio.Helpers
{
    public class PaginacaoHelper<T>
    {
      public static Resposta<Paginacao<T>> Paginar(Paginacao<T> entidadePaginada, IQueryable<T> query)
        {
            var total = query.Count();
            var queryPaginada = query.AsEnumerable()
                .Select((lnq, i) => new Paginacao<T>
                {
                    Entidade = lnq,
                    Posicao = i+1
                })
                .Skip(entidadePaginada.Posicao-1)
                .Take(1);

            var resultado = queryPaginada.ToList();

            entidadePaginada = resultado.FirstOrDefault();

            if (entidadePaginada == null)
                return new Resposta<Paginacao<T>>(null, "Não foram encontrados dados!");

            if (entidadePaginada.Entidade == null)
                return new Resposta<Paginacao<T>>(null, "Não foram encontrados dados!");

            entidadePaginada.Total = total;

            return new Resposta<Paginacao<T>>(entidadePaginada);
        }
    }
}
