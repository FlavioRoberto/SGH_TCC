using Data.Contexto;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.Disciplina>
    {
        private IContexto contexto;

        public DisciplinaRepositorio(IContexto contexto) : base(contexto)
        {
        }

        protected override Microsoft.EntityFrameworkCore.DbSet<Dominio.Model.DisciplinaModel.Disciplina> GetDbSet()
        {
            return contexto.Disciplina;
        }
    }
}
