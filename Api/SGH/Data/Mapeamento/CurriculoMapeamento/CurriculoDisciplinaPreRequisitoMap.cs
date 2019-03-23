

using Dominio.Model.CurriculoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento.CurriculoMapeamento
{
    public class CurriculoDisciplinaPreRequisitoMap : EntidadeMapeamento<CurriculoDisciplinaPreRequisito>
    {
        public CurriculoDisciplinaPreRequisitoMap(EntityTypeBuilder<CurriculoDisciplinaPreRequisito> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<CurriculoDisciplinaPreRequisito> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq=>lnq.Codigo)
                .HasColumnName("curpre_codigo");

            builder.Property(lnq => lnq.CodigoCurriculoDisciplina)
                .HasColumnName("curpre_curriculo_disciplina");

            builder.Property(lnq => lnq.CodigoDisciplina)
                .HasColumnName("curpre_disciplina");

            builder.ToTable("curriculo_disciplina_prerequisito");

            #region relacionamentos
            builder.HasOne(lnq => lnq.CurriculoDisciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinaPreRequisito)
                .HasForeignKey(lnq => lnq.CodigoCurriculoDisciplina);

            builder.HasOne(lnq => lnq.Disciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinaPreRequisito)
                .HasForeignKey(lnq => lnq.CodigoDisciplina);

            #endregion

            return this;
        }
    }
}
