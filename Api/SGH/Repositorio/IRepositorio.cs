using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        DbSet<T> DbSet { get; }
        Task<T> Criar(T entidade);
        Task<T> Listar(Expression<Func<T, bool>> query);
        Task<List<T>> ListarTodos();
        Task<T> Atualizar(T entidade);
        Task<bool> Remover(Expression<Func<T, bool>> query);
        Task<Resposta<Paginacao<T>>> ListarPorPaginacao(Paginacao<T> entidade);
    }
}
