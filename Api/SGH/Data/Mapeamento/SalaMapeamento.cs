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
                   .HasColumnName("Sala_Codigo");

            builder.Property(lnq => lnq.CodigoBloco)
                   .HasColumnName("Sala_Bloco");

            builder.Property(lnq => lnq.Descricao)
                   .HasColumnName("Sala_Descricao");

            builder.Property(lnq => lnq.Laboratorio)
                   .HasConversion<int>()
                   .HasColumnName("Sala_Laboratorio");

            builder.Property(lnq => lnq.Numero)
                   .HasColumnName("Sala_Numero");
            #endregion

            #region relacionamentos
            builder.HasOne(lnq => lnq.Bloco)
                   .WithMany(lnq => lnq.Salas)
                   .HasForeignKey(lnq => lnq.CodigoBloco);
            #endregion

            builder.ToTable("Sala");

            return this;
        }
    }
}
