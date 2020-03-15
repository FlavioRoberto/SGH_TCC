using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class BlocoRepositorio : IBlocoRepositorio
    {
        public IRepositorio<Bloco> _repositorioBase;

        public BlocoRepositorio(IRepositorio<Bloco> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task<Bloco> Atualizar(Bloco blocoEntidade)
        {
            return await _repositorioBase.Atualizar(blocoEntidade);
        }

        public async Task<bool> Contem(Expression<Func<Bloco, bool>> expressao)
        {
            return await _repositorioBase.Contem(expressao);
        }

        public async Task<Bloco> Criar(Bloco entidade)
        {
            return await _repositorioBase.Criar(entidade);
        }

        public async Task<bool> Remover(Expression<Func<Bloco, bool>> expressao)
        {
            return await _repositorioBase.Remover(expressao);
        }
    }
}
