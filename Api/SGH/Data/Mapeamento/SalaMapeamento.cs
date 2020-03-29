using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;
using SHG.Data.Mapeamento;

namespace SGH.Data.Mapeamento
{
    public class SalaMapeamento : EntidadeMapeamento<Sala>
    {
        public SalaMapeamento(EntityTypeBuilder<Sala> builder) : base(builder)
        { }

        public override EntidadeMapeamento<Sala> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            #region Properties

            builder.Property(lnq => lnq.Codigo)
                   .HasColumnName("sala_codigo");

            builder.Property(lnq => lnq.CodigoBloco)
                   .HasColumnName("sala_bloco");

            builder.Property(lnq => lnq.Descricao)
                   .HasColumnName("sala_descricao");

            builder.Property(lnq => lnq.Laboratorio)
                   .HasConversion<int>()
                   .HasColumnName("sala_laboratorio");

            builder.Property(lnq => lnq.Numero)
                   .HasColumnName("sala_numero");
            #endregion

            #region relacionamentos
            builder.HasOne(lnq => lnq.Bloco)
                   .WithMany(lnq => lnq.Salas)
                   .HasForeignKey(lnq => lnq.CodigoBloco);
            #endregion

            builder.ToTable("sala");

            return this;
        }
    }
}
