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
    public class CurriculoDisciplinaRepositorio : ICurriculoDisciplinaRepositorio
    {
        private readonly IRepositorio<CurriculoDisciplina> _repositorio;
        private readonly IContexto _contexto;

        public CurriculoDisciplinaRepositorio(IRepositorio<CurriculoDisciplina> repositorio,
                                              IContexto contexto)
        {
            _repositorio = repositorio;
            _contexto = contexto;
        }

        public async Task<CurriculoDisciplina> Consultar(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _contexto.CurriculoDisciplina
                                  .Include(lnq => lnq.Disciplina)
                                  .FirstOrDefaultAsync(expressao);
        }

        public async Task<bool> Contem(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _repositorio.Contem(expressao);
        }

        public async Task<CurriculoDisciplina> Criar(CurriculoDisciplina entidade)
        {
            return await _repositorio.Criar(entidade);
        }

        public async Task<CurriculoDisciplina> Atualizar(CurriculoDisciplina entidade)
        {
            var preRequisitos = await AtualizarPreRequisitos(entidade.Codigo, entidade.CurriculoDisciplinaPreRequisito.ToList());
            entidade.CurriculoDisciplinaPreRequisito = null;
            entidade = await _repositorio.Atualizar(entidade);
            return entidade;
        }

        private async Task<List<CurriculoDisciplinaPreRequisito>> AtualizarPreRequisitos(long codigoCurriculoDisciplina, List<CurriculoDisciplinaPreRequisito> curriculoDisciplinaPreRequisito)
        {
            var disciplinasBancoPreRequisito = await _contexto.CurriculoDisciplinaPreRequisito
                                                     .AsNoTracking()
                                                     .Where(lnq => lnq.CodigoCurriculoDisciplina == codigoCurriculoDisciplina)
                                                     .ToListAsync();

            _contexto.CurriculoDisciplinaPreRequisito.RemoveRange(disciplinasBancoPreRequisito);

            await _repositorio.SaveChangesAsync();

           _contexto.CurriculoDisciplinaPreRequisito.AddRange(curriculoDisciplinaPreRequisito);

            await _repositorio.SaveChangesAsync();

            return curriculoDisciplinaPreRequisito;

        }

        public async Task<List<CurriculoDisciplina>> Listar(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _contexto.CurriculoDisciplina
                                     .Include(lnq => lnq.Disciplina)
                                     .Include(lnq => lnq.CurriculoDisciplinaPreRequisito)
                                     .ThenInclude(lnq => lnq.Disciplina)
                                     .Where(expressao)
                                     .OrderBy(lnq => lnq.Periodo)
                                     .ToListAsync();
        }

        public async Task<bool> Remover(int codigo)
        {
            await _repositorio.IniciarTransacao();

            await RemoverPrerequisitosCurriculoDisciplina(codigo);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == codigo);

            _repositorio.FecharTransacao();

            return resultado;
        }

        private async Task RemoverPrerequisitosCurriculoDisciplina(int codigo)
        {
            var preRequisitos = await _contexto.CurriculoDisciplinaPreRequisito
                                                  .Where(lnq => lnq.CodigoCurriculoDisciplina == codigo)
                                                  .ToListAsync();

            _contexto.CurriculoDisciplinaPreRequisito.RemoveRange(preRequisitos);
        }

        public async Task<CurriculoDisciplinaPreRequisito> ConsultarPreRequisito(long codigoDisciplina)
        {
            return await _contexto.CurriculoDisciplinaPreRequisito
                                     .Include(lnq => lnq.Disciplina)
                                     .FirstOrDefaultAsync(lnq => lnq.CodigoDisciplina == codigoDisciplina);
        }

        public async Task<Disciplina> ConsultarDisciplinaVinculadaCurriculo(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            var codigoDisciplina = await _contexto.CurriculoDisciplina
                                                     .Where(expressao)
                                                     .Select(lnq => lnq.CodigoDisciplina)
                                                     .FirstOrDefaultAsync();
            if(codigoDisciplina.HasValue)
                return await _contexto.Disciplina.FirstOrDefaultAsync(lnq => lnq.Codigo == codigoDisciplina);

            return null;
        }
    }
}
