using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public class DisciplinaTipoMapeamento : EntidadeMapeamento<DisciplinaTipo>
    {
        public DisciplinaTipoMapeamento(EntityTypeBuilder<DisciplinaTipo> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<DisciplinaTipo> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo)
                .HasColumnName("Distip_Codigo");

            builder.Property(lnq => lnq.Descricao)
                .HasColumnName("Distip_Descricao");

            builder.ToTable("Disciplina_Tipo");

            #region relacionamentos
            builder.HasMany(lnq => lnq.Disciplinas)
                .WithOne(lnq => lnq.DisciplinaTipo)
                .HasForeignKey(lnq => lnq.CodigoTipo)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion


            return this;
        }
    }
}
