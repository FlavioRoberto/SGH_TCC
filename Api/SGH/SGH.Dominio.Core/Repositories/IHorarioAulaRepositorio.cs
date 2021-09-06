using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Repositories
{
    public interface IHorarioAulaRepositorio
    {
        Task<Horario> Criar(Horario entidade);
        Task<List<Horario>> Listar(ListarHorarioFiltro filtro);
        Task<List<Horario>> Listar(Expression<Func<Horario, bool>> expressao);
        Task<bool> Contem(Expression<Func<Horario, bool>> expressao);
        Task<bool> Remover(Expression<Func<Horario, bool>> query);
        Task<Horario> Atualizar(Horario entidade);
        Task<Turno> ConsultarTurno(int codigoHorario);
    }
}
