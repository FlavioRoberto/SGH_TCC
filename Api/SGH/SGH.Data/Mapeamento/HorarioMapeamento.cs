﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;
using SHG.Data.Mapeamento;

namespace SGH.Data.Mapeamento
{
    public class HorarioMapeamento : EntidadeMapeamento<Horario>
    {
        public HorarioMapeamento(EntityTypeBuilder<Horario> builder):base(builder)
        {
        }

        public override EntidadeMapeamento<Horario> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Ano)
                   .HasColumnName("Horario_Ano");

            builder.Property(lnq => lnq.Descricao)
                    .HasColumnName("Horario_Descricao");

            builder.Property(lnq => lnq.Codigo)
                   .HasColumnName("Horario_Codigo");

            builder.Property(lnq => lnq.CodigoCurriculo)
                   .HasColumnName("Horario_Curriculo");

            builder.Property(lnq => lnq.CodigoTurno)
                   .HasColumnName("Horario_Turno");

            builder.Property(lnq => lnq.Semestre)
                   .HasColumnName("Horario_Semestre");

            builder.Property(lnq => lnq.Periodo)
                   .HasColumnName("Horario_Periodo");

            builder.Property(lnq => lnq.Mensagem)
                   .HasColumnName("Horario_mensagem");

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

            builder.ToTable("Horarios_Aula");

            return this;
        }
    }
}
