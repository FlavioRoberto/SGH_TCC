using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICurriculoDisciplinaRepositorio
    {
        Task<CurriculoDisciplina> Consultar(Expression<Func<CurriculoDisciplina, bool>> expressao);
        Task<List<CurriculoDisciplina>> Listar(Expression<Func<CurriculoDisciplina, bool>> expressao);
        Task<bool> Contem(Expression<Func<CurriculoDisciplina, bool>> expressao);
        Task<CurriculoDisciplina> Criar(CurriculoDisciplina entidade);
        Task<bool> Remover(int codigo);

    }
}
