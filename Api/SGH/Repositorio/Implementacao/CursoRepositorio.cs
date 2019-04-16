using Data.Contexto;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao
{
    public class CursoRepositorio : RepositorioBase<Curso>
    {
        private IContexto _contexto;

        public CursoRepositorio(MySqlContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public override Resposta<Paginacao<Curso>> ListarPorPaginacao(Paginacao<Curso> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<Curso> GetDbSet()
        {
            return _contexto.Curso;
        }
    }
}
