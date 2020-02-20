using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICurriculoRepositorio : IRepositorioPaginacao<Curriculo>
    {
        Task<List<CurriculoDisciplina>> ListarDisciplinas(int curriculoId);
        Task<int> RetornarQuantidadeDisciplinaCurriculo(int codigoCurriculo);
        Task<CurriculoDisciplina> ConsultarCurriculoDisciplina(int codigoCurriculoDisciplina);
        Task<bool> Remover(int codigoCurriculo);
        Task<Curriculo> Atualizar(Curriculo entidade);
        Task<Curriculo> Criar(Curriculo entidade);
        Task<bool> Contem(Expression<Func<Curriculo, bool>> expressao);
        Task<List<Curriculo>> ListarTodos();
    }
}
