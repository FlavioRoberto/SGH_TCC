using Data.Contexto;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Implementacao
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        public DbSet<T> DbSet { get; }
        protected IContexto _contexto { get; private set; }
        protected abstract DbSet<T> GetDbSet();
        public abstract Task<Resposta<Paginacao<T>>> ListarPorPaginacao(Paginacao<T> entidade);
        
        public RepositorioBase(IContexto contexto)
        {
            _contexto = contexto;
            DbSet = GetDbSet();
        }

        public async Task<T> Atualizar(T entidade)
        {
            try
            {
                DbSet.Update(entidade);
                await _contexto.SaveChangesAsync();
                return entidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task<bool> ExisteEntidadeNoBanco(Expression<Func<T, bool>> query)
        {
            return await DbSet.CountAsync(query) > 0;
        }

        public async Task<T> Criar(T entidade)
        {
            try
            {
                DbSet.Add(entidade);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return entidade;
        }

        public async virtual Task<T> Listar(Expression<Func<T, bool>> query)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(query);
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public async virtual Task<List<T>> ListarPor(Expression<Func<T, bool>> query)
        {
            try
            {
                return await DbSet.AsNoTracking().Where(query).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public async Task<List<T>> ListarTodos()
        {
            try
            {
                return await DbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Remover(Expression<Func<T, bool>> query)
        {
            try
            {
                var item = await DbSet.FirstOrDefaultAsync(query);
                if (item != null)
                {
                    DbSet.Remove(item);
                    await _contexto.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                    throw new Exception("Não foi encontrado uma pessoa com o código informado!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

  
    }
}
