using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;
using SHG.Data.Mapeamento;

namespace SGH.Data.Mapeamento
{
    public class HorarioAulaMapeamento : EntidadeMapeamento<HorarioAula>
    {
        public HorarioAulaMapeamento(EntityTypeBuilder<HorarioAula> builder):base(builder)
        {
        }

        public override EntidadeMapeamento<HorarioAula> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Ano)
                   .HasColumnName("horario_ano");

            builder.Property(lnq => lnq.Codigo)
                   .HasColumnName("horario_codigo");

            builder.Property(lnq => lnq.CodigoCurriculo)
                   .HasColumnName("horario_curriculo");

            builder.Property(lnq => lnq.CodigoTurno)
                   .HasColumnName("horario_turno");

            builder.Property(lnq => lnq.Semestre)
                   .HasColumnName("horario_semestre");

            builder.Property(lnq => lnq.Periodo)
                   .HasColumnName("horario_periodo");

            builder.HasOne(lnq => lnq.Curriculo)
                   .WithMany(lnq => lnq.HorariosAula)
                   .HasForeignKey(lnq => lnq.CodigoCurriculo)
                   .HasPrincipalKey(lnq => lnq.Codigo)
                   .HasConstraintName("FK_Curriculo_Horario")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lnq => lnq.Turno)
                   .WithMany(lnq => lnq.HorariosAula)
                   .HasForeignKey(lnq => lnq.CodigoTurno)
                   .HasPrincipalKey(lnq => lnq.Codigo)
                   .HasConstraintName("FK_Turno_Horario")
                   .OnDelete(DeleteBehavior.Restrict);            

            builder.ToTable("horarios_aula");

            return this;
        }
    }
}
