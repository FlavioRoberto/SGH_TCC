﻿using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Repositories
{
    public interface ICargoDisciplinaRepositorio
    {
        Task<CargoDisciplina> Criar(CargoDisciplina entidade);
        Task<bool> Remover(Expression<Func<CargoDisciplina, bool>> expressao);
        Task<List<CargoDisciplina>> Listar(Expression<Func<CargoDisciplina, bool>> query);
        Task<List<CargoDisciplina>> ListarDisciplinasCurriculo(Expression<Func<CargoDisciplina, bool>> query);
        Task<CargoDisciplina> Atualizar(CargoDisciplina entidade);
        Task<bool> Contem(Expression<Func<CargoDisciplina, bool>> expressao);
        Task<Disciplina> RetornarDisciplina(long codigoCurriculoDisciplina);
        Task<Curriculo> RetornarCurriculoDisciplina(long codigoCurriculoDisciplina);
        Task<CargoDisciplina> Consultar(Expression<Func<CargoDisciplina, bool>> expressao);
        Task<Cargo> ConsultarCargo(long codigoDisciplina);
        Task IniciarTransacao();
        void FecharTransacao();

    }
}
