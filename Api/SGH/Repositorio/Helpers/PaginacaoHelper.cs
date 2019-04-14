using System.Linq;
using Dominio.ViewModel;

namespace Repositorio.Helpers
{
    public class PaginacaoHelper<T>
    {
      public static Paginacao<T> Paginar(Paginacao<T> entidadePaginada, IQueryable<T> query)
        {
            var total = query.Count();
            var queryPaginada = query.AsEnumerable()
                .Select((lnq, i) => new Paginacao<T>
                {
                    Entidade = lnq,
                    Posicao = i+1
                })
                .Skip(entidadePaginada.Posicao-1);

            var resultado = queryPaginada.ToList();

            entidadePaginada = resultado.FirstOrDefault();

            if (entidadePaginada != null)
                entidadePaginada.Total = total;
           
            if(entidadePaginada.Entidade == null)
                throw new System.Exception("Não foram encontrados dados!");
            
            return entidadePaginada;
        }
    }
}
