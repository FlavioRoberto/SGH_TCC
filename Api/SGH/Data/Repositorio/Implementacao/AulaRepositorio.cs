﻿using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly IRepositorio<Aula> _repositorioBase;
        private readonly IContexto _contexto;

        public AulaRepositorio(IRepositorio<Aula>  repositorioBase, IContexto contexto)
        {
            _repositorioBase = repositorioBase;
            _contexto = contexto;
        }

        public async Task<bool> Contem(Expression<Func<Aula, bool>> expressao)
        {
            return await _repositorioBase.Contem(expressao);
        }

        public async Task<Aula> Criar(Aula aula)
        {
            return await _repositorioBase.Criar(aula);
        }

        public async Task<List<Aula>> Listar(Expression<Func<Aula, bool>> expressao)
        {
            return await _repositorioBase.Listar(expressao);
        }

        public async Task<List<Aula>> ListarComDisciplinas(Expression<Func<Aula, bool>> expressao)
        {
            return await _contexto.Aula
                                  .Include(lnq => lnq.Disciplina)
                                  .Where(expressao)
                                  .ToListAsync();
        }

        public async Task<bool> Remover(Expression<Func<Aula, bool>> expressao)
        {
            return await _repositorioBase.Remover(expressao);
        }

        public async Task<bool> VerificarDisponibilidadeCargo(int codigoCargo, string diaSemana, string hora)
        {
            return await _contexto.Aula
                                  .Include(lnq => lnq.Disciplina)
                                  .AsNoTracking()
                                  .AnyAsync(lnq => lnq.Disciplina.CodigoCargo == codigoCargo &&
                                                   lnq.Reserva.Hora == hora &&
                                                   lnq.Reserva.DiaSemana == diaSemana); 
        }

        public async Task<bool> VerificarDisponibilidadeProfessor(int codigoProfessor, string diaSemana, string hora)
        {
            return await _contexto.Aula
                                  .Include(lnq => lnq.Disciplina)
                                  .ThenInclude(lnq => lnq.Cargo)
                                  .ThenInclude(lnq => lnq.Professor)
                                  .AsNoTracking()
                                  .AnyAsync(lnq => lnq.Disciplina.Cargo.Professor.Codigo == codigoProfessor &&
                                                   lnq.Reserva.Hora == hora &&
                                                   lnq.Reserva.DiaSemana == diaSemana);
        }
    }
}
