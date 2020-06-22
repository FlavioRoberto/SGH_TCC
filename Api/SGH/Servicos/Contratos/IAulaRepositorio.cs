using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Contratos
{
    public interface IAulaRepositorio
    {
        Task<Aula> Criar(Aula aula);
        Task<bool> Contem(Expression<Func<Aula, bool>> expressao);
        Task<List<Aula>> Listar(Expression<Func<Aula, bool>> expressao);
        Task<List<Aula>> ListarComDisciplinas(Expression<Func<Aula, bool>> expressao);              
        Task<bool> VerificarDisponibilidadeCargo(int codigoCargo, string diaSemana, string hora);
        Task<bool> VerificarDisponibilidadeProfessor(int codigoProfessor, string diaSemana, string hora);
        Task<bool> Remover(Expression<Func<Aula, bool>> expressao);
    }
}
