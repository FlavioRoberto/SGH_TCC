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
    public class CurriculoDisciplinaRepositorio : ICurriculoDisciplinaRepositorio
    {
        private readonly IRepositorio<CurriculoDisciplina> _repositorio;

        public CurriculoDisciplinaRepositorio(IRepositorio<CurriculoDisciplina> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<CurriculoDisciplina> Consultar(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _repositorio.GetDbSet<CurriculoDisciplina>()
                                     .Include(lnq => lnq.Disciplina)
                                     .FirstOrDefaultAsync(expressao);
        }

        public async Task<bool> Contem(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _repositorio.Contem(expressao);
        }

        public async Task<CurriculoDisciplina> Criar(CurriculoDisciplina entidade)
        {
            var curriculoDisciplina = await _repositorio.Criar(entidade);
            return curriculoDisciplina;
        }

        public async Task<List<CurriculoDisciplina>> Listar(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _repositorio.GetDbSet<CurriculoDisciplina>()
                                     .Include(lnq => lnq.Disciplina)
                                     .Include(lnq => lnq.CurriculoDisciplinaPreRequisito)
                                     .ThenInclude(lnq => lnq.Disciplina)
                                     .Where(expressao)
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
            var preRequisitos = await _repositorio.GetDbSet<CurriculoDisciplinaPreRequisito>()
                                                  .Where(lnq => lnq.CodigoCurriculoDisciplina == codigo)
                                                  .ToListAsync();

            _repositorio.GetDbSet<CurriculoDisciplinaPreRequisito>().RemoveRange(preRequisitos);
        }
    }
}
