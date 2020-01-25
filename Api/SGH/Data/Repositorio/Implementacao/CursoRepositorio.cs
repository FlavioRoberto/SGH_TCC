﻿using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
    {

        public CursoRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public async Task<Paginacao<Curso>> ListarPorPaginacao(Paginacao<Curso> entidadePaginada)
        {
            var query = _contexto.Curso.AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Curso>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade == null)
                entidade = new Curso();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            return await PaginacaoHelper<Curso>.Paginar(entidadePaginada, query);
        }

    }
}
