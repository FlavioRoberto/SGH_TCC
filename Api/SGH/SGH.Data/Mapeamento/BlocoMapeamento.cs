using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;
using SHG.Data.Mapeamento;

namespace SGH.Data.Mapeamento
{
    public class BlocoMapeamento : EntidadeMapeamento<Bloco>
    {
        public BlocoMapeamento(EntityTypeBuilder<Bloco> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Bloco> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            #region Propriedades
            builder.Property(lnq => lnq.Codigo)
                   .HasColumnName("Bloco_Codigo");

            builder.Property(lnq => lnq.Descricao)
                   .HasColumnName("Bloco_Descricao");
            #endregion

            builder.ToTable("Bloco");

            return this;
        }
    }
}
