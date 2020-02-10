using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Criar(T entidade);
        Task<T> Consultar(Expression<Func<T, bool>> query);
        Task<List<T>> ListarTodos();
        Task<List<T>> Listar(Expression<Func<T, bool>> query);
        Task<T> Atualizar(T entidade);
        Task<bool> Remover(Expression<Func<T, bool>> query);
        Task<bool> Contem(Expression<Func<T, bool>> expressao);
        DbSet<TData> GetDbSet<TData>() where TData : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
