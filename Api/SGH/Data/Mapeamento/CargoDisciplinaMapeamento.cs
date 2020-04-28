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
            builder.HasKey(lnq => lnq.Codigo);

            #region propriedades

            builder.Property(lnq => lnq.CodigoCargo)
                   .HasColumnName("Cardis_Cargo");

            builder.Property(lnq => lnq.CodigoCurriculoDisciplina)
                   .HasColumnName("Cardis_Disciplina");

            builder.Property(lnq => lnq.CodigoTurno)
                   .HasColumnName("Cardis_Turno");

            builder.Property(lnq => lnq.Descricao)
                   .HasColumnName("Cardis_Descricao");

            #endregion propriedades

            #region relacionamentos

            builder.HasOne(lnq => lnq.Disciplina)
                   .WithMany(lnq => lnq.Cargos)
                   .HasForeignKey(lnq => lnq.CodigoCurriculoDisciplina)
                   .HasConstraintName("FK_Cargo_Disciplina")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lnq => lnq.Cargo)
                   .WithMany(lnq => lnq.Disciplinas)
                   .HasForeignKey(lnq => lnq.CodigoCargo)
                   .HasConstraintName("FK_Cargo")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lnq => lnq.Turno)
                   .WithMany(lnq => lnq.DisciplinasCargo)
                   .HasForeignKey(lnq => lnq.CodigoTurno)
                   .HasConstraintName("Fk_Turno")
                   .OnDelete(DeleteBehavior.Restrict);

            #endregion relacionamentos

            builder.ToTable("Cargo_Disciplina");

            return this;
        }
    }
}
