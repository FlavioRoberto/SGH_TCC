using System;
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
            .HasColumnName("cargo_numero");

            builder.Property(lnq => lnq.Edital)
            .HasColumnName("cargo_edital");

            builder.Property(lnq => lnq.Ano)
            .HasColumnName("cargo_ano");

            builder.Property(lnq => lnq.Semestre)
            .HasColumnName("cargo_semestre");

            builder.Property(lnq => lnq.CodigoProfessor)
            .HasColumnName("cargo_professor");

            #endregion properties

            #region relacionamentos

            builder.HasOne(lnq => lnq.Professor)
            .WithMany(lnq => lnq.Cargos)
            .HasForeignKey(lnq => lnq.CodigoProfessor)
            .HasConstraintName("FK_Professor");

            #endregion relacionamentos

            return this;
        }
    }
}