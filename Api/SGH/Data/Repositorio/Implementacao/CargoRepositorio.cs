using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class CargoRepositorio :  ICargoRepositorio
    {
        private readonly IRepositorio<Cargo> _repositorio;

        public CargoRepositorio(IRepositorio<Cargo> repositorio) 
        {
            _repositorio = repositorio;
        }

        public Task<Cargo> Criar(Cargo entidade)
        {
            return _repositorio.Criar(entidade);
        }
      
        public async Task<bool> Contem(Expression<Func<Cargo, bool>> expressao)
        {
            return await _repositorio.Contem(expressao);
        }

        public async Task<bool> Remover(Expression<Func<Cargo, bool>> expressao)
        {
            return await _repositorio.Remover(expressao);
        }

        public async Task<Paginacao<Cargo>> ListarPorPaginacao(Paginacao<Cargo> entidadePaginada)
        {
            var query = _repositorio.GetDbSet<Cargo>()
                                    .Include(lnq => lnq.Professor)
                                    .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Cargo>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault() ?? new Cargo();

            if (entidade.Ano > 0)
                query = query.Where(lnq => lnq.Ano == entidade.Ano);

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoProfessor > 0)
                query = query.Where(lnq => lnq.CodigoProfessor == entidade.CodigoProfessor);

            if (entidade.Edital > 0)
                query = query.Where(lnq => lnq.Edital == entidade.Edital);

            if (entidade.Numero > 0)
                query = query.Where(lnq => lnq.Numero == entidade.Numero);

            if (entidade.Semestre > 0)
                query = query.Where(lnq => lnq.Semestre == entidade.Semestre);

            return await PaginacaoHelper<Cargo>.Paginar(entidadePaginada, query);
        }

        public async Task<Cargo> Atualizar(Cargo entidade)
        {
            return await _repositorio.Atualizar(entidade);
        }

        public async Task<Cargo> Consultar(Expression<Func<Cargo, bool>> expressao)
        {
            return await _repositorio.Consultar(expressao);
        }

        public async Task<Professor> ConsultarProfessor(long codigoCargo)
        {
            var cargo = await _repositorio.Consultar(lnq => lnq.Codigo == codigoCargo);
            return await _repositorio.GetDbSet<Professor>().FirstOrDefaultAsync(lnq => lnq.Codigo == cargo.CodigoProfessor);
        }

        public async Task<List<Cargo>> Listar(Expression<Func<Cargo, bool>> expressao)
        {
            return await _repositorio.Listar(expressao);
        }
    }
}
