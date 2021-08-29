using SGH.Dominio.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Dominio.Core.Repositories
{
    public interface ICargoRepositorio : IRepositorioPaginacao<Cargo>
    {
        Task<Cargo> Criar(Cargo entidade);
        Task<bool> Contem(Expression<Func<Cargo, bool>> expressao);
        Task<bool> Remover(Expression<Func<Cargo, bool>> expressao);
        Task<Cargo> Atualizar(Cargo entidade);
        Task<Cargo> Consultar(Expression<Func<Cargo, bool>> expressao);
        Task<Professor> ConsultarProfessor(long codigoCargo);
        Task<List<Cargo>> Listar(Expression<Func<Cargo, bool>> expressao);
    }
}
