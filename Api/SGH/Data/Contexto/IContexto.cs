using Dominio.Model;
using Dominio.Model.Autenticacao;
using Dominio.Model.CurriculoModel;
using Dominio.Model.DisciplinaModel;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contexto
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
        DbSet<ProfessorCurso> ProfessorCurso { get; set; }

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();

    }
}
