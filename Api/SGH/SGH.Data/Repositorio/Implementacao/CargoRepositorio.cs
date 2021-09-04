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
    public class CargoRepositorio : RepositorioBase<Cargo>, ICargoRepositorio
    {
        public CargoRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public async Task<Paginacao<Cargo>> ListarPorPaginacao(Paginacao<Cargo> entidadePaginada)
        {
            var query = _contexto.Cargo
                                 .Include(lnq => lnq.Professor)
                                 .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Cargo>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault() ?? new Cargo();

            if (entidade.Ano > 0)
                query = query.Where(lnq => lnq.Ano == entidade.Ano);

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoProfessor > 0)
                query = query.Where(lnq => lnq.CodigoProfessor == entidade.CodigoProfessor);

            if (!string.IsNullOrEmpty(entidade.Edital))
                query = query.Where(lnq => lnq.Edital.Contains(entidade.Edital));

            if (entidade.Numero > 0)
                query = query.Where(lnq => lnq.Numero == entidade.Numero);

            if (entidade.Semestre > 0)
                query = query.Where(lnq => lnq.Semestre == entidade.Semestre);

            return await PaginacaoHelper<Cargo>.Paginar(entidadePaginada, query);
        }

        public Task<List<Professor>> ConsultarProfessor(List<long> codigosCargo)
        {
            return _contexto.Cargo
                            .Include(lnq => lnq.Professor)
                            .AsNoTracking()
                            .Where(lnq => codigosCargo.Contains(lnq.Codigo))
                            .Select(lnq => lnq.Professor)
                            .ToListAsync();
        }

        public Task<List<Cargo>> Listar(List<long> codigosCargos)
        {
            return _contexto.Cargo.Include(lnq => lnq.Professor).AsNoTracking().Where(x => codigosCargos.Contains(x.Codigo)).ToListAsync();
        }
    }
}
