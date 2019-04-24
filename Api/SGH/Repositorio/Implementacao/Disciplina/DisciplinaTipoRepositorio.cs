using Data.Contexto;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaTipoRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.DisciplinaTipo>
    {

        public DisciplinaTipoRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override Resposta<Paginacao<DisciplinaTipo>> ListarPorPaginacao(Paginacao<DisciplinaTipo> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<DisciplinaTipo> GetDbSet()
        {
            return _contexto.DisciplinaTipo;
        }
    }
}
