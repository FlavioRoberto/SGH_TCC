using Data.Contexto;
using Dominio.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Implementacao
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        private DbSet<T> _dbSet;
        protected IContexto _contexto { get; private set; }
        protected abstract DbSet<T> GetDbSet();
        public abstract Paginacao<T> ListarPorPaginacao(Paginacao<T> entidade);

        public RepositorioBase(IContexto contexto)
        {
            _contexto = contexto;
            _dbSet = GetDbSet();
        }

        public async Task<T> Atualizar(T entidade)
        {
            try
            {
                _dbSet.Update(entidade);
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
            return await _dbSet.CountAsync(query) > 0;
        }

        public async Task<T> Criar(T entidade)
        {
            try
            {
                _dbSet.Add(entidade);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return entidade;
        }

        public async Task<T> Listar(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(query);
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
                return await _dbSet.AsNoTracking().ToListAsync();
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
                var item = await _dbSet.FirstOrDefaultAsync(query);
                if (item != null)
                {
                    _dbSet.Remove(item);
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
