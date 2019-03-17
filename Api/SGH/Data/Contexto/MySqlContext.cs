using Data.Mapeamento;
using Data.Model;
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
            modelBuilder.Entity<Turno>(b => new TurnoMapeamento(b).Map());
        }

        public DbSet<Turno> Turno { get; set; }

    }
}
