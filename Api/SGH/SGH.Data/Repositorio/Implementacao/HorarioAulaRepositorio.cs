using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Repositories;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class HorarioAulaRepositorio : IHorarioAulaRepositorio
    {
        private readonly IContexto _contexto;
        private readonly IRepositorio<Horario> _repositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public HorarioAulaRepositorio(IContexto contexto, IRepositorio<Horario> repositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _contexto = contexto;
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<Horario> Atualizar(Horario entidade)
        {
            return await _repositorio.Atualizar(entidade);
        }

        public async Task<Turno> ConsultarTurno(int codigoHorario)
        {
            var horario = await _repositorio.Consultar(lnq => lnq.Codigo == codigoHorario);

            if (horario == null)
                return null;

            return await _contexto.Turno.FirstOrDefaultAsync(lnq => lnq.Codigo == horario.Codigo);
        }

        public async Task<bool> Contem(Expression<Func<Horario, bool>> expressao)
        {
            return await _repositorio.Contem(expressao);
        }

        public async Task<Horario> Criar(Horario entidade)
        {
            return await _repositorio.Criar(entidade);
        }

        public async Task<List<Horario>> Listar(ListarHorarioFiltro filtro)
        {
            var query = _contexto.HorarioAula
                                    .Include(lnq => lnq.Curriculo)
                                    .AsNoTracking();

            var usuario = await _usuarioRepositorio.Consultar(lnq => lnq.Codigo == filtro.codigoUsuario);

            if (usuario != null)
                query = query.Where(lnq => lnq.Curriculo.CodigoCurso == usuario.CursoCodigo);

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

        public async Task<List<Horario>> Listar(Expression<Func<Horario, bool>> expressao)
        {
            return await _contexto.HorarioAula
                                     .Where(expressao)
                                     .OrderBy(lnq => (int)lnq.Periodo)
                                     .ToListAsync();
        }

        public async Task<bool> Remover(Expression<Func<Horario, bool>> expressao)
        {
            return await _repositorio.Remover(expressao);
        }
    }
}
