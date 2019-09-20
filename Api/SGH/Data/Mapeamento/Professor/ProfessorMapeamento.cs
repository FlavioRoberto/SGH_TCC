using Dominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento
{
    public class ProfessorMapeamento : EntidadeMapeamento<Professor>
    {
        public ProfessorMapeamento(EntityTypeBuilder<Professor> builder) : base(builder)
        { }

        public override EntidadeMapeamento<Professor> Map()
        {
            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Codigo)
              .ValueGeneratedOnAdd()
              .HasColumnName("prof_codigo");

            builder.Property(p => p.Nome)
              .HasColumnName("prof_nome")
              .IsRequired(true)
              .HasMaxLength(45);

            builder.Property(p => p.Email)
              .HasColumnName("prof_email")
              .IsRequired(true)
              .HasMaxLength(50);

            builder.Property(p => p.Telefone)
              .HasColumnName("prof_telefone")
              .HasMaxLength(12);

            builder.Property(p => p.Matricula)
              .HasColumnName("prof_matricula")
              .HasMaxLength(10);

            builder.ToTable("professor");

            return this;
        }
    }
}
