using Dominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento
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
                .HasColumnName("turno_codigo")
                .ValueGeneratedOnAdd();

            builder.Property(lnq => lnq.Descricao)
                .HasColumnName("turno_descricao")
                .IsRequired();

            builder.ToTable("turno");

            return this;
        }
    }
}
