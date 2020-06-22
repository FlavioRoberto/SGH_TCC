using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core.Contratos;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected IContexto _contexto { get; set; }

        public RepositorioBase(IContexto contexto)
        {
            _contexto = contexto;
        }

        public async virtual Task<T> Atualizar(T entidade)
        {
            try
            {
                _contexto.Set<T>().Update(entidade);
                await _contexto.SaveChangesAsync();
                return entidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async virtual Task<T> Criar(T entidade)
        {
            try
            {
                _contexto.Set<T>().Add(entidade);
                await _contexto.SaveChangesAsync();
                return entidade;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async virtual Task<T> Consultar(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _contexto.Set<T>().AsNoTracking().FirstOrDefaultAsync(query);
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public async virtual Task<List<T>> Listar(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _contexto.Set<T>().AsNoTracking().Where(query).ToListAsync();
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
                return await _contexto.Set<T>().AsNoTracking().ToListAsync();
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
                var item = await _contexto.Set<T>().FirstOrDefaultAsync(query);
                if (item != null)
                {
                    _contexto.Set<T>().Remove(item);
                    await _contexto.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("Não foi encontrado a entidade com o código informado!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Contem(Expression<Func<T, bool>> expressao)
        {
            return await _contexto.Set<T>().AsNoTracking().CountAsync(expressao) > 0;
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return _contexto.Set<T>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _contexto.SaveChangesAsync(cancellationToken);
        }

        public async Task IniciarTransacao()
        {
           await _contexto.IniciarTransacao();
        }

        public void FecharTransacao()
        {
            _contexto.FecharTransacao();
        }
    }
}
