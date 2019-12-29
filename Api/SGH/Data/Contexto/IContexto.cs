using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SHG.Data.Contexto
{
    public interface IContexto
    {
        DbSet<CurriculoDisciplina> CurriculoDisciplina { get; set; }
        DbSet<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }

        DbSet<Curriculo> Curriculo { get; set; }
        DbSet<Disciplina> Disciplina { get; set; }
        DbSet<DisciplinaTipo> DisciplinaTipo { get; set; }
        DbSet<Curso> Curso { get; set; }
        DbSet<Turno> Turno { get; set; }
        DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        DbSet<Usuario> Usuario { get; set; }
        DbSet<Professor> Professor { get; set; }
        DbSet<Cargo> Cargo { get; set; }
        DbSet<CargoDisciplina> CargoDisciplina { get; set; }

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();

    }
}
