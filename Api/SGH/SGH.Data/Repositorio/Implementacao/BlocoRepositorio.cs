using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Helpers;
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
    public class BlocoRepositorio : IBlocoRepositorio
    {
        public IRepositorio<Bloco> _repositorioBase;
        private readonly IContexto _contexto;

        public BlocoRepositorio(IRepositorio<Bloco> repositorioBase,
                                IContexto contexto)
        {
            _repositorioBase = repositorioBase;
            _contexto = contexto;
        }

        public async Task<Bloco> Atualizar(Bloco blocoEntidade)
        {
            return await _repositorioBase.Atualizar(blocoEntidade);
        }

        public async Task<bool> Contem(Expression<Func<Bloco, bool>> expressao)
        {
            return await _repositorioBase.Contem(expressao);
        }

        public async Task<Bloco> Criar(Bloco entidade)
        {
            return await _repositorioBase.Criar(entidade);
        }

        public async Task<Paginacao<Bloco>> ListarPorPaginacao(Paginacao<Bloco> entidadePaginada)
        {
            var query = _contexto.Bloco
                                 .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Bloco>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault() ?? new Bloco();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));
            
            return await PaginacaoHelper<Bloco>.Paginar(entidadePaginada, query);
        }

        public async Task<List<Bloco>> ListarTodos()
        {
            return await _repositorioBase.ListarTodos();
        }

        public async Task<bool> Remover(Expression<Func<Bloco, bool>> expressao)
        {
            return await _repositorioBase.Remover(expressao);
        }
    }
}
