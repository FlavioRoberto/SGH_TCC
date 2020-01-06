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
                .HasColumnName("distip_codigo");

            builder.Property(lnq => lnq.Descricao)
                .HasColumnName("distip_descricao");

            builder.ToTable("disciplina_tipo");


            #region relacionamentos
            builder.HasMany(lnq => lnq.Disciplinas)
                .WithOne(lnq => lnq.DisciplinaTipo)
                .HasForeignKey(lnq => lnq.CodigoTipo);
            #endregion

            return this;
        }
    }
}
