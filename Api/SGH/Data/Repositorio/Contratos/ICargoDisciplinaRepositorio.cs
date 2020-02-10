using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICargoDisciplinaRepositorio
    {
        Task<CargoDisciplina> Criar(CargoDisciplina entidade);
        Task<bool> Remover(Expression<Func<CargoDisciplina, bool>> expressao);
    }
}
