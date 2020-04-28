using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public class CurriculoDisciplinaMapeamento : EntidadeMapeamento<CurriculoDisciplina>
    {
        public CurriculoDisciplinaMapeamento(EntityTypeBuilder<CurriculoDisciplina> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<CurriculoDisciplina> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            #region Properties

            builder.Property(lnq => lnq.Codigo)
                .HasColumnName("Curdis_Codigo");

            builder.Property(lnq => lnq.CodigoDisciplina)
                .HasColumnName("Curdis_Disciplina")
                .IsRequired(false);

            builder.Property(lnq => lnq.Periodo)
              .IsRequired()
              .HasColumnName("Curdis_Periodo");

            builder.Property(lnq => lnq.CodigoCurriculo)
                .HasColumnName("Curdis_Curriculo");

            builder.Property(lnq => lnq.AulasSemanaisPratica)
                .HasColumnName("Curdis_Quantidade_Aulas_Semanal_Pratica");

            builder.Property(lnq => lnq.AulasSemanaisTeorica)
                .HasColumnName("Curdis_Quantidade_Aulas_Semanais_Teorica");

            #endregion

            builder.ToTable("Curriculo_Disciplina");

            #region relacionamentos

            builder.HasOne(lnq => lnq.Curriculo)
                .WithMany(lnq => lnq.Disciplinas)
                .HasForeignKey(lnq => lnq.CodigoCurriculo)
                .HasConstraintName("FK_Curriculo")
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(lnq => lnq.Disciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinas)
                .HasForeignKey(lnq => lnq.CodigoDisciplina)
                .HasConstraintName("FK_Disciplina")
                .OnDelete(DeleteBehavior.Restrict);

            #endregion relacionamentos

            return this;
        }
    }
}
