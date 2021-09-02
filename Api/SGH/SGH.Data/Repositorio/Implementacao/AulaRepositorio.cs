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
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly IContexto _contexto;
        private readonly IRepositorio<Aula> _repositorioBase;

        public AulaRepositorio(IContexto contexto, IRepositorio<Aula>  repositorioBase)
        {
            _contexto = contexto;
            _repositorioBase = repositorioBase;
        }

        public async Task<bool> Atualizar(Aula aula)
        {
            var resultado = await _repositorioBase.Atualizar(aula);
            return resultado != null && resultado.Codigo > 0;
        }

        public Task<Aula> Consultar(long aulaId)
        {
            return _repositorioBase.Consultar(lnq => lnq.Codigo == aulaId);
        }

        public async Task<bool> Contem(Expression<Func<Aula, bool>> expressao)
        {
            return await _repositorioBase.Contem(expressao);
        }

        public async Task<Aula> Criar(Aula aula)
        {
            return await _repositorioBase.Criar(aula);
        }

        public async Task<List<Aula>> Listar(Expression<Func<Aula, bool>> expressao)
        {
            return await _contexto.Aula
                                  .Include(lnq => lnq.DisciplinasAuxiliar)
                                  .ThenInclude(lnq => lnq.Disciplina)
                                  .ThenInclude(lnq => lnq.Cargo)
                                  .ThenInclude(lnq => lnq.Professor)
                                  .Where(expressao)
                                  .ToListAsync();
        }

        public async Task<List<Aula>> ListarAulasAuxiliares(IEnumerable<long> codigosDisciplinas)
        {
            return await _contexto.Aula
                                  .Include(lnq => lnq.DisciplinasAuxiliar)
                                  .ThenInclude(lnq => lnq.Disciplina)
                                  .ThenInclude(lnq => lnq.Cargo)
                                  .ThenInclude(lnq => lnq.Professor)
                                  .Where(lnq => lnq.DisciplinasAuxiliar.Any(x => codigosDisciplinas.Contains(x.CodigoCargoDisciplina)))
                                  .ToListAsync();
        }

        public async Task<List<Aula>> ListarComDisciplinas(Expression<Func<Aula, bool>> expressao)
        {
            return await _contexto.Aula
                                         .Include(lnq => lnq.Disciplina)
                                         .Include(lnq => lnq.Reserva)
                                         .OrderBy(lnq => lnq.Reserva.DiaSemana)
                                         .ThenBy(lnq => TimeSpan.Parse(lnq.Reserva.Hora))
                                         .Where(expressao)
                                         .ToListAsync();
        }

        public async Task<bool> Remover(Expression<Func<Aula, bool>> expressao)
        {
            return await _repositorioBase.Remover(expressao);
        }

        public async Task<bool> VerificarDisponibilidadeCargo(long codigoCargo, string diaSemana, string hora)
        {
            return await _contexto.Aula
                                        .Include(lnq => lnq.Disciplina)
                                        .AsNoTracking()
                                        .AnyAsync(lnq => lnq.Disciplina.CodigoCargo == codigoCargo &&
                                                         lnq.Reserva.Hora == hora &&
                                                         lnq.Reserva.DiaSemana == diaSemana); 
        }

        public async Task<bool> VerificarDisponibilidadeProfessor(long codigoProfessor, string diaSemana, string hora)
        {
            return await _contexto.Aula
                                    .Include(lnq => lnq.Disciplina)
                                    .ThenInclude(lnq => lnq.Cargo)
                                    .ThenInclude(lnq => lnq.Professor)
                                    .AsNoTracking()
                                    .AnyAsync(lnq => lnq.Disciplina.Cargo.Professor.Codigo == codigoProfessor &&
                                                     lnq.Reserva.Hora == hora &&
                                                     lnq.Reserva.DiaSemana == diaSemana);
        }
    }
}
