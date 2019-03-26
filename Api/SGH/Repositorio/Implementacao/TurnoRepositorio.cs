using Data.Contexto;
using Dominio.Model;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao
{
    public class TurnoRepositorio : RepositorioBase<Turno>
    {
        private IContexto _contexto;

        public TurnoRepositorio(IContexto contexto) : base(contexto)
        {
            this._contexto = contexto;
        }

        protected override DbSet<Turno> GetDbSet()
        {
            return _contexto.Turno;
        }
    }
}
