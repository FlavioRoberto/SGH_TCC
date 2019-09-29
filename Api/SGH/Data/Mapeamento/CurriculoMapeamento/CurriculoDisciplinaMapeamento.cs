﻿using Dominio.Model.CurriculoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento.CurriculoMapeamento
{
    public class CurriculoDisciplinaMapeamento : EntidadeMapeamento<CurriculoDisciplina>
    {
        public CurriculoDisciplinaMapeamento(EntityTypeBuilder<CurriculoDisciplina> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<CurriculoDisciplina> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            #region Properties

            builder.Property(lnq => lnq.Codigo)
                .HasColumnName("curdis_codigo");

            builder.Property(lnq => lnq.CodigoDisciplina)
                .HasColumnName("curdis_disciplina")
                .IsRequired(false);

            builder.Property(lnq => lnq.CodigoCurriculo)
                .HasColumnName("curdis_curriculo");

            builder.Property(lnq => lnq.AulasSemanaisPratica)
                .HasColumnName("curdis_quantidade_aulas_semanal_pratica");

            builder.Property(lnq => lnq.AulasSemanaisTeorica)
                .HasColumnName("curdis_quantidade_aulas_semanais_teorica");

            builder.Property(lnq => lnq.Credito)
                .HasColumnName("curdis_credito");

            #endregion

            builder.ToTable("curriculo_disciplina");

            #region relacionamentos

            builder.HasOne(lnq => lnq.Curriculo)
                .WithMany(lnq => lnq.Disciplinas)
                .HasForeignKey(lnq => lnq.CodigoCurriculo)
                .HasConstraintName("FK_Curriculo");

            builder.HasOne(lnq => lnq.Disciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinas)
                .HasForeignKey(lnq => lnq.CodigoDisciplina)
                .HasConstraintName("FK_Disciplina");

            #endregion relacionamentos

            return this;
        }
    }
}
