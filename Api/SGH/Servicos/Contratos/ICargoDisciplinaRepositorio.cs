using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Contratos
{
    public interface ICargoDisciplinaRepositorio
    {
        Task<CargoDisciplina> Criar(CargoDisciplina entidade);
        Task<bool> Remover(Expression<Func<CargoDisciplina, bool>> expressao);
        Task<List<CargoDisciplina>> Listar(Expression<Func<CargoDisciplina, bool>> query);
        Task<List<CargoDisciplina>> ListarDisciplinasCurriculo(Expression<Func<CargoDisciplina, bool>> query);
        Task<CargoDisciplina> Atualizar(CargoDisciplina entidade);
        Task<bool> Contem(Expression<Func<CargoDisciplina, bool>> expressao);
        Task<Disciplina> RetornarDisciplina(int codigoCurriculoDisciplina);
        Task<Curriculo> RetornarCurriculoDisciplina(int codigoCurriculoDisciplina);
        Task<CargoDisciplina> Consultar(Expression<Func<CargoDisciplina, bool>> expressao);
        Task<Cargo> ConsultarCargo(int codigoDisciplina);

    }
}
