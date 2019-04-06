using Data.Contexto;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaTipoRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.DisciplinaTipo>
    {
        private IContexto _contexto;

        public DisciplinaTipoRepositorio(MySqlContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public override Paginacao<DisciplinaTipo> ListarPorPaginacao(Paginacao<DisciplinaTipo> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<DisciplinaTipo> GetDbSet()
        {
            return _contexto.DisciplinaTipo;
        }
    }
}
