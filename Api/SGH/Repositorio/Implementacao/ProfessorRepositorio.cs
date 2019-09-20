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
            var query = GetDbSet().Include(lnq=>lnq.Cursos).AsNoTracking();

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

        public async override Task<Professor> Criar(Professor entidade)
        {
            var cursosAdicionar = entidade.Cursos;
            entidade.Cursos = null;
            DbSet.Add(entidade);
            await _contexto.SaveChangesAsync();

            if (cursosAdicionar.Count > 0)
            {
                cursosAdicionar.ForEach(curso => 
                    curso.ProfessorId = entidade.Codigo                
                );
                _contexto.ProfessorCurso.AddRange(cursosAdicionar);
                await _contexto.SaveChangesAsync();
                entidade.Cursos.AddRange(cursosAdicionar);
            }

            return entidade;
            }

        public async override Task<Professor> Atualizar(Professor entidade)
        {
            try
            {
                var cursosAdicionar = entidade.Cursos;
                entidade.Cursos = null;
                DbSet.Update(entidade);
                var cursosRemover = await _contexto.ProfessorCurso.Where(lnq => lnq.ProfessorId == entidade.Codigo).ToListAsync();
                _contexto.ProfessorCurso.RemoveRange(cursosRemover);
                _contexto.ProfessorCurso.AddRange(cursosAdicionar);
                await _contexto.SaveChangesAsync();
                return entidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async override Task<Professor> Listar(Expression<Func<Professor, bool>> query)
        {
            try
            {
                return await DbSet.AsNoTracking()
                    .Include(lnq => lnq.Cursos)
                    .FirstOrDefaultAsync(query);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async override Task<List<Professor>> ListarPor(Expression<Func<Professor, bool>> query)
        {
            try
            {
                return await DbSet
                    .Include(lnq => lnq.Cursos)
                    .AsNoTracking()
                    .Where(query)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        protected override DbSet<Professor> GetDbSet()
        {
            return _contexto.Professor;
        }
    }
}
