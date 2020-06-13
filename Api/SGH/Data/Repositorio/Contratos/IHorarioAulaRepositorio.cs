using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IHorarioAulaRepositorio
    {
        Task<HorarioAula> Criar(HorarioAula entidade);
        Task<List<HorarioAula>> Listar(ListarHorarioFiltro filtro);
        Task<List<HorarioAula>> Listar(Expression<Func<HorarioAula, bool>> expressao);
        Task<bool> Contem(Expression<Func<HorarioAula, bool>> expressao);
        Task<bool> Remover(Expression<Func<HorarioAula, bool>> query);
        Task<HorarioAula> Atualizar(HorarioAula entidade);
    }
}
