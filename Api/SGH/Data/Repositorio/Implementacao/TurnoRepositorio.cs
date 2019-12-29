using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class TurnoRepositorio : RepositorioBase<Turno>
    {
        public TurnoRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public override async Task<Resposta<Paginacao<Turno>>> ListarPorPaginacao(Paginacao<Turno> entidadePaginada)
        {
            var query = _contexto.Turno.AsNoTracking();
            
            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            return await PaginacaoHelper<Turno>.Paginar(entidadePaginada, query);
        }

        protected override DbSet<Turno> GetDbSet()
        {
            return _contexto.Turno;
        }
    }
}
