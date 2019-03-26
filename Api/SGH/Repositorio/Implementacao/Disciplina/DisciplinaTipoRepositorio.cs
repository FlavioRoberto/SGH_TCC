using Data.Contexto;
using Dominio.Model.DisciplinaModel;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaTipoRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.DisciplinaTipo>
    {
        private IContexto _contexto;

        public DisciplinaTipoRepositorio(IContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        protected override DbSet<DisciplinaTipo> GetDbSet()
        {
            return _contexto.DisciplinaTipo;
        }
    }
}
