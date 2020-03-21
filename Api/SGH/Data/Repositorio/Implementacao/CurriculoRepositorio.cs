using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class CurriculoRepositorio : ICurriculoRepositorio
    {
        private readonly IRepositorio<Curriculo> _repositorio;

        public CurriculoRepositorio(IRepositorio<Curriculo> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Curriculo> Atualizar(Curriculo entidade)
        {
            return await _repositorio.Atualizar(entidade);
        }

        public async Task<Curriculo> Criar(Curriculo entidade)
        {
            return await _repositorio.Criar(entidade);
        }

        public async Task<Paginacao<Curriculo>> ListarPorPaginacao(Paginacao<Curriculo> entidadePaginada)
        {
            var query = _repositorio.GetDbSet<Curriculo>()
                                    .Include(lnq => lnq.Curso)
                                    .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Curriculo>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Ano > 0)
                query = query.Where(lnq => lnq.Ano == entidade.Ano);

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoCurso > 0)
                query = query.Where(lnq => lnq.CodigoCurso == entidade.CodigoCurso);

            return await PaginacaoHelper<Curriculo>.Paginar(entidadePaginada, query);
        }

        public async Task<int> RetornarQuantidadeDisciplinaCurriculo(int codigoCurriculo)
        {
            return await _repositorio.GetDbSet<CurriculoDisciplina>().CountAsync(lnq => lnq.CodigoCurriculo == codigoCurriculo);
        }

        public async Task<bool> Remover(int codigoCurriculo)
        {
            return await _repositorio.Remover(lnq => lnq.Codigo == codigoCurriculo);
        }

        public async Task<bool> Contem(Expression<Func<Curriculo, bool>> expressao)
        {
            return await _repositorio.Contem(expressao);
        }

        public async Task<List<Curriculo>> ListarTodos()
        {
            return await _repositorio.GetDbSet<Curriculo>().AsNoTracking().Include(lnq => lnq.Curso).ToListAsync();
        }

        public async Task<Curriculo> Consultar(Expression<Func<Curriculo, bool>> expressao)
        {
            return await _repositorio.Consultar(expressao);
        }
    }
}
