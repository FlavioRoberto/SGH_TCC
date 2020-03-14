using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
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

        public async Task<Bloco> Criar(Bloco entidade)
        {
            return await _repositorioBase.Criar(entidade);
        }
    }
}
