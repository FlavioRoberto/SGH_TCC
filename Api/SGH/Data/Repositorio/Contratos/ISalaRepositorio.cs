using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ISalaRepositorio
    {
        Task<bool> Contem(Expression<Func<Sala, bool>> expressao);
        Task<Sala> Criar(Sala salaEntidade);
        Task<Sala> Atualizar(Sala sala);
        Task<bool> Remover(Expression<Func<Sala, bool>> expressao);
        Task<Paginacao<Sala>> ListarPorPaginacao(Paginacao<Sala> entidadePaginada);
    }
}
