using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;

namespace SHG.Data.Mapeamento
{
    public class CargoDisciplinaMapeamento : EntidadeMapeamento<CargoDisciplina>
    {
        public CargoDisciplinaMapeamento(EntityTypeBuilder<CargoDisciplina> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<CargoDisciplina> Map()
        {
            builder.HasKey(lnq => new { lnq.CodigoCargo, lnq.CodigoCurriculoDisciplina });

            #region propriedades

            builder.Property(lnq => lnq.CodigoCargo)
            .HasColumnName("cardis_cargo");

            builder.Property(lnq => lnq.CodigoCurriculoDisciplina)
            .HasColumnName("cardis_disciplina");

            #endregion propriedades

            #region relacionamentos

            builder.HasOne(lnq => lnq.CurriculoDisciplina)
            .WithMany(lnq => lnq.Cargos)
            .HasForeignKey(lnq => lnq.CodigoCurriculoDisciplina)
            .HasConstraintName("FK_Cargo_Disciplina");

            builder.HasOne(lnq => lnq.Cargo)
            .WithMany(lnq => lnq.Disciplinas)
            .HasForeignKey(lnq => lnq.CodigoCargo)
            .HasConstraintName("FK_Cargo");

            #endregion relacionamentos

            return this;
        }
    }
}
