using Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.CurriculoImplementacao
{
    public class CurriculoRepositorio : RepositorioBase<Dominio.Model.Curriculo>
    {
        private IContexto _contexto;

        public CurriculoRepositorio(IContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        protected override DbSet<Dominio.Model.Curriculo> GetDbSet()
        {
            return _contexto.Curriculo;
        }
    }
}
