using Data.Contexto;
using Dominio.Model;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao
{
    public class CursoRepositorio : RepositorioBase<Curso>
    {
        private IContexto _contexto;

        public CursoRepositorio(IContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        protected override DbSet<Curso> GetDbSet()
        {
            return _contexto.Curso;
        }
    }
}
