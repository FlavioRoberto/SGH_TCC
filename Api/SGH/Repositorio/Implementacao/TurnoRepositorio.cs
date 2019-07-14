using Data.Contexto;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Implementacao
{
    public class TurnoRepositorio : RepositorioBase<Turno>
    {
        public TurnoRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override async Task<Resposta<Paginacao<Turno>>> ListarPorPaginacao(Paginacao<Turno> entidadePaginada)
        {
            var query = _contexto.Turno.AsNoTracking();
            
            var entidade = entidadePaginada.Entidade;

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
