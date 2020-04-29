using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IAulaRepositorio
    {
        Task<Aula> Criar(Aula aula);
        Task<bool> Contem(Expression<Func<Aula, bool>> expressao);
        Task<bool> VerificarDisponibilidadeCargo(int codigoCargo, string diaSemana, string hora);
        Task<bool> VerificarDisponibilidadeProfessor(int codigoProfessor, string diaSemana, string hora);
    }
}
