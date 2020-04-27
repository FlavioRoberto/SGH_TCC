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
    public class SalaRepositorio : ISalaRepositorio
    {
        private readonly IRepositorio<Sala> _repositorioBase;

        public SalaRepositorio(IRepositorio<Sala> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task<Sala> Atualizar(Sala sala)
        {
            return await _repositorioBase.Atualizar(sala);
        }

        public async Task<bool> Contem(Expression<Func<Sala, bool>> expressao)
        {
            return await _repositorioBase.Contem(expressao);
        }

        public async Task<Sala> Criar(Sala salaEntidade)
        {
            return await _repositorioBase.Criar(salaEntidade);
        }

        public async Task<Paginacao<Sala>> ListarPorPaginacao(Paginacao<Sala> entidadePaginada)
        {
            var query = _repositorioBase.GetDbSet<Sala>().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Sala>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault() ?? new Sala();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoBloco > 0)
                query = query.Where(lnq => lnq.CodigoBloco == entidade.CodigoBloco);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            if (entidade.Laboratorio)
                query = query.Where(lnq => lnq.Laboratorio == entidade.Laboratorio);

            if (entidade.Numero > 0)
                query = query.Where(lnq => lnq.Numero == entidade.Numero);

            return await PaginacaoHelper<Sala>.Paginar(entidadePaginada, query);
        }

        public async Task<List<Sala>> ListarTodos()
        {
            return await _repositorioBase.ListarTodos();
        }

        public async Task<bool> Remover(Expression<Func<Sala, bool>> expressao)
        {
            return await _repositorioBase.Remover(expressao);
        }
    }
}
