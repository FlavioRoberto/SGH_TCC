using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class HorarioAulaRepositorio : IHorarioAulaRepositorio
    {
        private readonly IRepositorio<HorarioAula> _repositorio;

        public HorarioAulaRepositorio(IRepositorio<HorarioAula> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<HorarioAula> Atualizar(HorarioAula entidade)
        {
            return await _repositorio.Atualizar(entidade);
        }

        public async Task<Turno> ConsultarTurno(int codigoHorario)
        {
            var horario = await _repositorio.Consultar(lnq => lnq.Codigo == codigoHorario);

            if (horario == null)
                return null;

            return await _repositorio.GetDbSet<Turno>().FirstOrDefaultAsync(lnq => lnq.Codigo == horario.Codigo);
        }

        public async Task<bool> Contem(Expression<Func<HorarioAula, bool>> expressao)
        {
            return await _repositorio.Contem(expressao);
        }

        public async Task<HorarioAula> Criar(HorarioAula entidade)
        {
            return await _repositorio.Criar(entidade);
        }

        public async Task<List<HorarioAula>> Listar(ListarHorarioFiltro filtro)
        {
            var query = _repositorio.GetDbSet<HorarioAula>().AsNoTracking();

            if (filtro.Ano.HasValue)
                query = query.Where(lnq => lnq.Ano == filtro.Ano);

            if (filtro.CodigoCurriculo.HasValue)
                query = query.Where(lnq => lnq.CodigoCurriculo == filtro.CodigoCurriculo);
          
            if (filtro.Periodo.HasValue)
                query = query.Where(lnq => lnq.Periodo == filtro.Periodo);
          
            if (filtro.Semestre.HasValue)
                query = query.Where(lnq => lnq.Semestre == filtro.Semestre);

            return await query.Include(lnq => lnq.Curriculo)
                              .ThenInclude(lnq => lnq.Curso)
                              .Include(lnq => lnq.Turno)
                              .OrderByDescending(lnq => lnq.Ano)
                              .ThenByDescending(lnq => lnq.Periodo)
                              .ThenBy(lnq => lnq.CodigoCurriculo)
                              .ToListAsync();
        }

        public async Task<List<HorarioAula>> Listar(Expression<Func<HorarioAula, bool>> expressao)
        {
            return await _repositorio.GetDbSet<HorarioAula>()
                                     .Where(expressao)
                                     .OrderBy(lnq => lnq.Periodo)
                                     .ToListAsync();
        }

        public async Task<bool> Remover(Expression<Func<HorarioAula, bool>> expressao)
        {
            return await _repositorio.Remover(expressao);
        }
    }
}
