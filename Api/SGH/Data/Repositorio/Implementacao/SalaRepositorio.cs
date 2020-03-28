using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class SalaRepositorio : ISalaRepositorio
    {
        private readonly IRepositorio<Sala> _repositorioBase;

        public SalaRepositorio(IRepositorio<Sala> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task<Sala> Atualizar(Sala sala)
        {
            return await _repositorioBase.Atualizar(sala);
        }

        public async Task<bool> Contem(Expression<Func<Sala, bool>> expressao)
        {
            return await _repositorioBase.Contem(expressao);
        }

        public async Task<Sala> Criar(Sala salaEntidade)
        {
            return await _repositorioBase.Criar(salaEntidade);
        }
    }
}
