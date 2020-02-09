﻿using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class CargoDisciplinaRepositorio : ICargoDisciplinaRepositorio
    {
        private readonly IRepositorio<CargoDisciplina> _repositorio;

        public CargoDisciplinaRepositorio(IRepositorio<CargoDisciplina> repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<CargoDisciplina> Criar(CargoDisciplina entidade)
        {
            return _repositorio.Criar(entidade);
        }
    }
}