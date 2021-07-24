using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public class CurriculoDisciplinaPreRequisitoMapeamento : EntidadeMapeamento<CurriculoDisciplinaPreRequisito>
    {
        public CurriculoDisciplinaPreRequisitoMapeamento(EntityTypeBuilder<CurriculoDisciplinaPreRequisito> builder) : base(builder)
        { }

        public override EntidadeMapeamento<CurriculoDisciplinaPreRequisito> Map()
        {
            builder.HasKey(lnq => new { lnq.CodigoCurriculoDisciplina, lnq.CodigoDisciplina });

            #region properties

            builder.Property(lnq => lnq.CodigoCurriculoDisciplina)
                .HasColumnName("DisPre_Curriculo_Disciplina");

            builder.Property(lnq => lnq.CodigoDisciplina)
                .HasColumnName("DisPre_Disciplina");

            #endregion

            builder.ToTable("Curriculo_Disciplina_Pre_Requisito");

            #region relacionamentos

            builder.HasOne(lnq => lnq.CurriculoDisciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinaPreRequisito)
                .HasForeignKey(lnq => lnq.CodigoCurriculoDisciplina)
                .HasConstraintName("FK_Curriculo_Disciplina")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lnq => lnq.Disciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinaPreRequisito)
                .HasForeignKey(lnq => lnq.CodigoDisciplina)
                .HasConstraintName("FK_Curriculo_Disciplina_Pre_Req")
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            return this;
        }
    }
}
