using System.Linq;
using Dominio.ViewModel;

namespace Repositorio.Helpers
{
    public class PaginacaoHelper<T>
    {
      public static Paginacao<T> Paginar(Paginacao<T> entidadePaginada, IQueryable<T> query)
        {
            var result = query.AsEnumerable()
                .Select((lnq,i) => new Paginacao<T> {            
                    Entidade = lnq,
                    Posicao = i})
                .Skip(entidadePaginada.Posicao)
                .FirstOrDefault();

            result.Total = query.Count();

            return result;
        }
    }
}
