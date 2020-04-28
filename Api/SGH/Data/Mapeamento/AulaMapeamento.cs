using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;
using SHG.Data.Mapeamento;

namespace SGH.Data.Mapeamento
{
    public class AulaMapeamento : EntidadeMapeamento<Aula>
    {
        public AulaMapeamento(EntityTypeBuilder<Aula> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Aula> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo)
                   .HasColumnName("Aula_Codigo");

            builder.Property(lnq => lnq.CodigoDisciplina)
                   .HasColumnName("Aula_Disciplina");

            builder.Property(lnq => lnq.CodigoHorario)
                   .HasColumnName("Aula_Horarario");

            builder.Property(lnq => lnq.CodigoSala)
                   .HasColumnName("Aula_Sala");

            builder.Property(lnq => lnq.DescricaoDesdobramento)
                   .HasColumnName("Aula_Descricao_Desdobramento");

            builder.Property(lnq => lnq.Desdobramento)
                   .HasColumnName("Aula_Desdobramento")
                   .HasConversion<int>();

            builder.OwnsOne(lnq => lnq.Reserva)
                   .Property(lnq => lnq.DiaSemana)
                   .HasColumnName("Aula_Dia_Semana");

            builder.OwnsOne(lnq => lnq.Reserva)
                   .Property(lnq => lnq.Hora)
                   .HasColumnName("Aula_Hora");

            builder.HasOne(lnq => lnq.Horario)
                   .WithMany(lnq => lnq.Aulas)
                   .HasForeignKey(lnq => lnq.CodigoHorario)
                   .HasConstraintName("FK_Horario_Aula");

            builder.HasOne(lnq => lnq.Sala)
                   .WithMany(lnq => lnq.Aulas)
                   .HasForeignKey(lnq => lnq.CodigoSala)
                   .HasConstraintName("FK_Sala_Aula");

            builder.HasOne(lnq => lnq.Disciplina)
                   .WithMany(lnq => lnq.Aulas)
                   .HasForeignKey(lnq => lnq.CodigoDisciplina)
                   .HasConstraintName("FK_Cargo_Disciplina_Aula");

            builder.ToTable("Aulas");

            return this;
        }
    }
}
