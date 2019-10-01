using Data.Contexto;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contratos;
using Repositorio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositorio.Implementacao
{
    public class ProfessorRepositorio : RepositorioBase<Professor>, IProfessorRepositorio
    {
        public ProfessorRepositorio(MySqlContext contexto):base(contexto)
        { }

        public override async Task<Resposta<Paginacao<Professor>>> ListarPorPaginacao(Paginacao<Professor> entidadePaginada)
        {
            var query = GetDbSet().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new Professor();

            var entidade = entidadePaginada.Entidade;

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


        protected override DbSet<Professor> GetDbSet()
        {
            return _contexto.Professor;
        }
    }
}
