using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Linq;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class TurnoRepositorio : RepositorioBase<Turno>, ITurnoRepositorio
    {
        public TurnoRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public async Task<Paginacao<Turno>> ListarPorPaginacao(Paginacao<Turno> entidadePaginada)
        {
            var query = _contexto.Turno.AsNoTracking();
            
            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            return await PaginacaoHelper<Turno>.Paginar(entidadePaginada, query);
        }
    }
}
