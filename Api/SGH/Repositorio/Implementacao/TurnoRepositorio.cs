﻿using Data.Contexto;
using Dominio.Model;
using Dominio.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;
using System;
using System.Linq;

namespace Repositorio.Implementacao
{
    public class TurnoRepositorio : RepositorioBase<Turno>
    {
        public TurnoRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override Paginacao<Turno> ListarPorPaginacao(Paginacao<Turno> entidadePaginada)
        {
            var query = _contexto.Turno.AsNoTracking();
            
            var entidade = entidadePaginada.Entidade;

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));

            return PaginacaoHelper<Turno>.Paginar(entidadePaginada, query);
        }

        protected override DbSet<Turno> GetDbSet()
        {
            return _contexto.Turno;
        }
    }
}
