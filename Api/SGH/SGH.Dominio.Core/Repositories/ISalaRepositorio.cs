﻿using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Repositories
{
    public interface ISalaRepositorio
    {
        Task<bool> Contem(Expression<Func<Sala, bool>> expressao);
        Task<Sala> Criar(Sala salaEntidade);
        Task<Sala> Atualizar(Sala sala);
        Task<bool> Remover(Expression<Func<Sala, bool>> expressao);
        Task<Paginacao<Sala>> ListarPorPaginacao(Paginacao<Sala> entidadePaginada);
        Task<List<Sala>> ListarTodos();
        Task<Sala> Consultar(Expression<Func<Sala, bool>> expressao);
    }
}
