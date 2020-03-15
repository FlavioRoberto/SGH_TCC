using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IBlocoRepositorio
    {
        Task<Bloco> Criar(Bloco entidade);
        Task<Bloco> Atualizar(Bloco blocoEntidade);
        Task<bool> Contem(Expression<Func<Bloco, bool>> expressao);
        Task<bool> Remover(Expression<Func<Bloco, bool>> expressao);
        Task<Paginacao<Bloco>> ListarPorPaginacao(Paginacao<Bloco> entidadePaginada);

    }
}
