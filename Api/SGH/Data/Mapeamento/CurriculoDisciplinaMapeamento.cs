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
                .HasColumnName("curdis_codigo");

            builder.Property(lnq => lnq.CodigoDisciplina)
                .HasColumnName("curdis_disciplina")
                .IsRequired(false);

            builder.Property(lnq => lnq.Periodo)
              .IsRequired()
              .HasColumnName("curdis_periodo");

            builder.Property(lnq => lnq.CodigoCurriculo)
                .HasColumnName("curdis_curriculo");

            builder.Property(lnq => lnq.AulasSemanaisPratica)
                .HasColumnName("curdis_quantidade_aulas_semanal_pratica");

            builder.Property(lnq => lnq.AulasSemanaisTeorica)
                .HasColumnName("curdis_quantidade_aulas_semanais_teorica");

            #endregion

            builder.ToTable("curriculo_disciplina");

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
