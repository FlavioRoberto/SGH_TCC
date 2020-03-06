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
            return await _repositorio.Criar(entidade);
        }

        public async Task<List<CurriculoDisciplina>> Listar(Expression<Func<CurriculoDisciplina, bool>> expressao)
        {
            return await _repositorio.GetDbSet<CurriculoDisciplina>()
                                     .Include(lnq => lnq.Disciplina)
                                     .Where(expressao)
                                     .ToListAsync();
        }

        public async Task<bool> Remover(Expression<Func<CurriculoDisciplina, bool>> query)
        {
            return await _repositorio.Remover(query);
        }
    }
}
