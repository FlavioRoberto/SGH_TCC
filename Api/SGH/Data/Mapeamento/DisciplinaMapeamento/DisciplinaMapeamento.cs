
using Microsoft.EntityFrameworkCore;

namespace SHG.Data.Mapeamento
{
    public class DisciplinaMapeamento : EntidadeMapeamento<Dominio.Model.DisciplinaModel.Disciplina>
    {
        public DisciplinaMapeamento(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Dominio.Model.DisciplinaModel.Disciplina> builder) : base(builder)
        {
           
        }

        public override EntidadeMapeamento<Dominio.Model.DisciplinaModel.Disciplina> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo).HasColumnName("dis_codigo");
            builder.Property(lnq => lnq.Descricao).HasColumnName("dis_descricao");
            builder.Property(lnq => lnq.CodigoTipo).HasColumnName("dis_tipo");

            builder.ToTable("disciplina");

            #region relacionamentos

            builder.HasOne(lnq => lnq.DisciplinaTipo)
                .WithMany(lnq => lnq.Disciplinas)
                .HasForeignKey(lnq => lnq.CodigoTipo);

            #endregion

            return this;
        }
    }
}
