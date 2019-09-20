using Dominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento
{
    public class ProfessorCursoMapeamento : EntidadeMapeamento<ProfessorCurso>
    {
        public ProfessorCursoMapeamento(EntityTypeBuilder<ProfessorCurso> builder) : base(builder)
        { }

        public override EntidadeMapeamento<ProfessorCurso> Map()
        {
            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Codigo)
                .HasColumnName("profcur_codigo");

            builder.Property(p => p.ProfessorId)
                .HasColumnName("profcur_professor");

            builder.Property(p => p.CursoId)
                .HasColumnName("profcur_curso");

            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Professores)
                .HasForeignKey(p => p.CursoId);

            builder.HasOne(p => p.Professor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(p => p.ProfessorId);

            builder.ToTable("professor_curso");

            return this;
        }
    }
}
