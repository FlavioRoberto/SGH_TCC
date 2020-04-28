using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public class TurnoMapeamento : EntidadeMapeamento<Turno>
    {

        public TurnoMapeamento(EntityTypeBuilder<Turno> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Turno> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo)
                .HasColumnName("Turno_Codigo")
                .ValueGeneratedOnAdd();

            builder.Property(lnq => lnq.Descricao)
                .HasColumnName("Turno_Descricao")
                .IsRequired();

            builder.ToTable("Turno");

            return this;
        }
    }
}
