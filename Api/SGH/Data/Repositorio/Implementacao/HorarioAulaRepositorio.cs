using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class HorarioAulaRepositorio : IHorarioAulaRepositorio
    {
        private readonly IRepositorio<HorarioAula> _repositorio;

        public HorarioAulaRepositorio(IRepositorio<HorarioAula> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<HorarioAula> Criar(HorarioAula entidade)
        {
            return await _repositorio.Criar(entidade);
        }
    }
}
