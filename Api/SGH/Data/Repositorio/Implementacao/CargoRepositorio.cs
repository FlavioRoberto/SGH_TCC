using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System;
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

        public Task<Paginacao<Cargo>> ListarPorPaginacao(Paginacao<Cargo> entidadePaginada)
        {
            throw new NotImplementedException();
        }

    }
}
