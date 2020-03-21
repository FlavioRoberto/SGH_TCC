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
    public class CargoDisciplinaRepositorio : ICargoDisciplinaRepositorio
    {
        private readonly IRepositorio<CargoDisciplina> _repositorio;

        public CargoDisciplinaRepositorio(IRepositorio<CargoDisciplina> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CargoDisciplina> Atualizar(CargoDisciplina entidade)
        {
            return await _repositorio.Atualizar(entidade);
        }

        public async Task<CargoDisciplina> Consultar(Expression<Func<CargoDisciplina, bool>> expressao)
        {
            return await _repositorio.Consultar(expressao);
        }

        public Task<bool> Contem(Expression<Func<CargoDisciplina, bool>> expressao)
        {
            return _repositorio.Contem(expressao);
        }

        public async Task<CargoDisciplina> Criar(CargoDisciplina entidade)
        {
            return await _repositorio.Criar(entidade);
        }

        public async Task<List<CargoDisciplina>> Listar(Expression<Func<CargoDisciplina, bool>> query)
        {
            return await _repositorio.GetDbSet<CargoDisciplina>()
                                     .Include(lnq => lnq.Turno)
                                     .AsNoTracking()
                                     .Where(query)
                                     .ToListAsync();
        }

        public async Task<bool> Remover(Expression<Func<CargoDisciplina, bool>> expressao)
        {
            return await _repositorio.Remover(expressao);
        }

        public async Task<Curriculo> RetornarCurriculoDisciplina(int codigoCurriculoDisciplina)
        {
            var curriculoDisciplina = await _repositorio.GetDbSet<CurriculoDisciplina>()
                .Include(lnq => lnq.Curriculo)
                .ThenInclude(lnq => lnq.Curso)
                .AsNoTracking()
                .FirstOrDefaultAsync(lnq => lnq.Codigo == codigoCurriculoDisciplina);

            return curriculoDisciplina.Curriculo;
        }

        public async Task<Disciplina> RetornarDisciplina(int codigoCurriculoDisciplina)
        {
            var curriculoDisciplina = await _repositorio.GetDbSet<CurriculoDisciplina>()
                .Include(lnq => lnq.Disciplina)
                .AsNoTracking()
                .FirstOrDefaultAsync(lnq => lnq.Codigo == codigoCurriculoDisciplina);

            return curriculoDisciplina.Disciplina;
        }
    }
}
