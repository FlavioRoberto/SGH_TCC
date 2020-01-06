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
                .HasColumnName("disPre_curriculo_disciplina");

            builder.Property(lnq => lnq.CodigoDisciplina)
                .HasColumnName("disPre_disciplina");

            #endregion

            builder.ToTable("curriculo_disciplina_pre_requisito");

            #region relacionamentos

            builder.HasOne(lnq => lnq.CurriculoDisciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinaPreRequisito)
                .HasForeignKey(lnq => lnq.CodigoCurriculoDisciplina)
                .HasConstraintName("FK_Curriculo_Disciplina");

            builder.HasOne(lnq => lnq.Disciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinaPreRequisito)
                .HasForeignKey(lnq => lnq.CodigoDisciplina)
                .HasConstraintName("FK_Curriculo_Disciplina_Pre_Req");

            #endregion

            return this;
        }
    }
}
