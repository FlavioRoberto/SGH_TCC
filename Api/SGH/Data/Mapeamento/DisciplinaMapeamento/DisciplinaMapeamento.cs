
using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core.Model;

namespace SHG.Data.Mapeamento
{
    public class DisciplinaMapeamento : EntidadeMapeamento<Disciplina>
    {
        public DisciplinaMapeamento(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Disciplina> builder) : base(builder)
        {
           
        }

        public override EntidadeMapeamento<Disciplina> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo).HasColumnName("dis_codigo");
            builder.Property(lnq => lnq.Descricao).HasColumnName("dis_descricao");
            builder.Property(lnq => lnq.CodigoTipo).HasColumnName("dis_tipo");

            builder.ToTable("disciplina");

            #region relacionamentos

            builder.HasOne(lnq => lnq.DisciplinaTipo)
                .WithMany(lnq => lnq.Disciplinas)
                .HasForeignKey(lnq => lnq.CodigoTipo)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            return this;
        }
    }
}
