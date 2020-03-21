using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICurriculoRepositorio : IRepositorioPaginacao<Curriculo>
    {
        Task<int> RetornarQuantidadeDisciplinaCurriculo(int codigoCurriculo);
        Task<bool> Remover(int codigoCurriculo);
        Task<Curriculo> Atualizar(Curriculo entidade);
        Task<Curriculo> Criar(Curriculo entidade);
        Task<bool> Contem(Expression<Func<Curriculo, bool>> expressao);
        Task<List<Curriculo>> ListarTodos();
        Task<Curriculo> Consultar(Expression<Func<Curriculo, bool>> expressao);
    }
}
