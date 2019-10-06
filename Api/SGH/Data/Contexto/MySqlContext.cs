﻿using Data.Mapeamento;
using Data.Mapeamento.AutenticacaoMapeamento;
using Data.Mapeamento.CargoMapeamento;
using Data.Mapeamento.CurriculoMapeamento;
using Data.Mapeamento.Disciplina;
using Data.Mapeamento.DisciplinaMapeamento;
using Dominio.Model;
using Dominio.Model.Autenticacao;
using Dominio.Model.CurriculoModel;
using Dominio.Model.DisciplinaModel;
using Microsoft.EntityFrameworkCore;


namespace Data.Contexto
{
    public class MySqlContext : DbContext, IContexto
    {
        public MySqlContext()
        { }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurriculoDisciplina>(b => new CurriculoDisciplinaMapeamento(b).Map());
            modelBuilder.Entity<CurriculoDisciplinaPreRequisito>(b => new CurriculoDisciplinaPreRequisitoMapeamento(b).Map());
            modelBuilder.Entity<Curriculo>(b => new CurriculoMapeamento(b).Map());
            modelBuilder.Entity<Disciplina>(b => new DisciplinaMapeamento(b).Map());
            modelBuilder.Entity<DisciplinaTipo>(b => new DisciplinaTipoMapeamento(b).Map());
            modelBuilder.Entity<Curso>(b => new CursoMapeamento(b).Map());
            modelBuilder.Entity<Turno>(b => new TurnoMapeamento(b).Map());
            modelBuilder.Entity<UsuarioPerfil>(b => new UsuarioPerfilMapeamento(b).Map());
            modelBuilder.Entity<Usuario>(b => new UsuarioMapeamento(b).Map());
            modelBuilder.Entity<Professor>(b => new ProfessorMapeamento(b).Map());
            modelBuilder.Entity<Cargo>(b => new CargoMapeamento(b).Map());
            modelBuilder.Entity<CargoDisciplina>(b => new CargoDisciplinaMapeamento(b).Map());
        }

        public DbSet<CurriculoDisciplina> CurriculoDisciplina { get; set; }
        public DbSet<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }
        public DbSet<Curriculo> Curriculo { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<DisciplinaTipo> DisciplinaTipo { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CargoDisciplina> CargoDisciplina { get; set; }
    }
}
