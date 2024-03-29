﻿using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Repositories
{
    public interface ICurriculoDisciplinaRepositorio
    {
        Task<CurriculoDisciplina> Consultar(Expression<Func<CurriculoDisciplina, bool>> expressao);
        Task<List<CurriculoDisciplina>> Listar(Expression<Func<CurriculoDisciplina, bool>> expressao);
        Task<bool> Contem(Expression<Func<CurriculoDisciplina, bool>> expressao);
        Task<CurriculoDisciplina> Criar(CurriculoDisciplina entidade);
        Task<CurriculoDisciplina> Atualizar(CurriculoDisciplina entidade);
        Task<bool> Remover(int codigo);
        Task<CurriculoDisciplinaPreRequisito> ConsultarPreRequisito(long codigoDisciplina);
        Task<Disciplina> ConsultarDisciplinaVinculadaCurriculo(Expression<Func<CurriculoDisciplina, bool>> expressao);
    }
}
