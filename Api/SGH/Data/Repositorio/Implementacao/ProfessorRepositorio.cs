using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class ProfessorRepositorio : RepositorioBase<Professor>, IProfessorRepositorio
    {
        public ProfessorRepositorio(IContexto contexto):base(contexto)
        { }

        public async Task<Paginacao<Professor>> ListarPorPaginacao(Paginacao<Professor> entidadePaginada)
        {
            var query = GetDbSet<Professor>().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Professor>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Email))
                query = query.Where(lnq => lnq.Email.Contains(entidade.Email));

            if (!string.IsNullOrEmpty(entidade.Nome))
                query = query.Where(lnq => lnq.Nome.Contains(entidade.Nome));

            if (!string.IsNullOrEmpty(entidade.Telefone))
                query = query.Where(lnq => lnq.Telefone.Contains(entidade.Telefone));

            return await PaginacaoHelper<Professor>.Paginar(entidadePaginada, query);
        }

    }
}
