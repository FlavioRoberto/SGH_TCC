using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public class CargoMapeamento : EntidadeMapeamento<Cargo>
    {
        public CargoMapeamento(EntityTypeBuilder<Cargo> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Cargo> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            #region properties

            builder.Property(lnq => lnq.Numero)
            .HasColumnName("Cargo_Mumero");

            builder.Property(lnq => lnq.Edital)
            .HasColumnName("Cargo_Edital");

            builder.Property(lnq => lnq.Ano)
            .HasColumnName("Cargo_Ano");

            builder.Property(lnq => lnq.Semestre)
            .HasColumnName("Cargo_Semestre");

            builder.Property(lnq => lnq.CodigoProfessor)
            .HasColumnName("Cargo_Professor");

            #endregion properties

            #region relacionamentos

            builder.HasOne(lnq => lnq.Professor)
            .WithMany(lnq => lnq.Cargos)
            .HasForeignKey(lnq => lnq.CodigoProfessor)
            .HasConstraintName("FK_Professor")
            .OnDelete(DeleteBehavior.Restrict);

            #endregion relacionamentos

            builder.ToTable("Cargo");

            return this;
        }
    }
}