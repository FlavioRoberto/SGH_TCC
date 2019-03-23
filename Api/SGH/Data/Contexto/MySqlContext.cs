using Data.Mapeamento;
using Data.Mapeamento.CurriculoMapeamento;
using Data.Mapeamento.Disciplina;
using Data.Mapeamento.DisciplinaMapeamento;
using Dominio.Model;
using Dominio.Model.CurriculoModel;
using Dominio.Model.DisciplinaModel;
using Microsoft.EntityFrameworkCore;


namespace Data.Contexto
{
    public class MySqlContext : DbContext
    {
        public MySqlContext()
        {}

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurriculoDisciplina>(b => new CurriculoDisciplinaMapeamento(b).Map());
            modelBuilder.Entity<CurriculoDisciplinaPreRequisito>(b => new CurriculoDisciplinaPreRequisitoMap(b).Map());
            modelBuilder.Entity<Curriculo>(b => new CurriculoMapeamento(b).Map());
            modelBuilder.Entity<Disciplina>(b => new DisciplinaMapeamento(b).Map());
            modelBuilder.Entity<DisciplinaTipo>(b => new DisciplinaTipoMapeamento(b).Map());
            modelBuilder.Entity<Curso>(b => new CursoMapeamento(b).Map());
            modelBuilder.Entity<Turno>(b => new TurnoMapeamento(b).Map());
        }

        public DbSet<Turno> Turno { get; set; }

    }
}
