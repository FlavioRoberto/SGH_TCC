using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ISalaRepositorio
    {
        Task<bool> Contem(Expression<Func<Sala, bool>> expressao);
    }
}
