using Data.Contexto;
using Dominio.Model.CurriculoModel;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Curriculo
{
    public class CurriculoDisciplinaRepositorio : RepositorioBase<CurriculoDisciplina>
    {
        private IContexto _contexto;

        public CurriculoDisciplinaRepositorio(IContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        protected override DbSet<CurriculoDisciplina> GetDbSet()
        {
            return _contexto.CurriculoDisciplina;
        }
    }
}
