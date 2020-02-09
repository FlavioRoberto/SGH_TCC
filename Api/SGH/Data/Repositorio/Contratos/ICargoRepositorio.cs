using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICargoRepositorio : IRepositorioPaginacao<Cargo>
    {
        Task<Cargo> Criar(Cargo entidade);
        Task<bool> Contem(Expression<Func<Cargo, bool>> expressao);
    }
}
