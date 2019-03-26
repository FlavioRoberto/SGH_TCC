using Data.Contexto;
using Dominio.Model.CurriculoModel;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Curriculo
{
    public class CurDisPreRequisitoRepositorio : RepositorioBase<CurriculoDisciplinaPreRequisito>
    {
        private IContexto _contexto;

        public CurDisPreRequisitoRepositorio(IContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        protected override DbSet<CurriculoDisciplinaPreRequisito> GetDbSet()
        {
            return _contexto.CurriculoDisciplinaPreRequisito;
        }
    }
}
