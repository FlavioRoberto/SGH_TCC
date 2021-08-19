using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
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
              .HasColumnName("Prof_Codigo");

            builder.Property(p => p.Nome)
              .HasColumnName("Prof_Nome")
              .IsRequired(true)
              .HasMaxLength(45);

            builder.Property(p => p.Email)
              .HasColumnName("Prof_Email")
              .IsRequired(true);

            builder.Property(p => p.Telefone)
              .HasColumnName("Prof_Telefone")
              .HasMaxLength(12);

            builder.Property(p => p.Matricula)
              .HasColumnName("Prof_Matricula");

            builder.Property(p => p.Contratacao)
                   .HasColumnName("Prof_Contratacao");

            builder.Property(p => p.Ativo)
              .HasConversion<int>()
              .HasColumnName("Prof_Ativo")
              .HasMaxLength(10);

            builder.ToTable("Professor");

            return this;
        }
    }
}
