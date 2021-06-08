using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICurriculoRepositorio : IRepositorioPaginacao<Curriculo>
    {
        Task<int> RetornarQuantidadeDisciplinaCurriculo(long codigoCurriculo);
        Task<bool> Remover(long codigoCurriculo);
        Task<Curriculo> Atualizar(Curriculo entidade);
        Task<Curriculo> Criar(Curriculo entidade);
        Task<bool> Contem(Expression<Func<Curriculo, bool>> expressao);
        Task<List<Curriculo>> ListarTodos();
        Task<Curriculo> Consultar(Expression<Func<Curriculo, bool>> expressao);
        Task<IList<long>> ListarCodigos(Expression<Func<Curriculo, bool>> expressao);
        Task<Curso> ConsultarCurso(long codigoCurriculo);
    }
}
