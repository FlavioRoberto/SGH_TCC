using Data.Contexto;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.Disciplina>
    {
        private IContexto contexto;

        public DisciplinaRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override Paginacao<Dominio.Model.DisciplinaModel.Disciplina> ListarPorPaginacao(Paginacao<Dominio.Model.DisciplinaModel.Disciplina> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override Microsoft.EntityFrameworkCore.DbSet<Dominio.Model.DisciplinaModel.Disciplina> GetDbSet()
        {
            return contexto.Disciplina;
        }
    }
}
