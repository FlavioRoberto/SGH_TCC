using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IAulaRepositorio
    {
        Task<Aula> Criar(Aula aula);
        Task<bool> Contem(Expression<Func<Aula, bool>> expressao);
        Task<List<Aula>> Listar(Expression<Func<Aula, bool>> expressao);
        Task<List<Aula>> ListarComDisciplinas(Expression<Func<Aula, bool>> expressao);              
        Task<bool> VerificarDisponibilidadeCargo(long codigoCargo, string diaSemana, string hora);
        Task<bool> VerificarDisponibilidadeProfessor(long codigoProfessor, string diaSemana, string hora);
        Task<bool> Remover(Expression<Func<Aula, bool>> expressao);
        Task<Aula> Consultar(long aulaId);
        Task<bool> Atualizar(Aula aula);
    }
}
