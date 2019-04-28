using Dominio.Model.CurriculoModel;
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
                .HasColumnName("curdis_disciplina");

            builder.Property(lnq => lnq.CodigoCurriculo)
                .HasColumnName("curdis_curriculo");

            builder.Property(lnq => lnq.CargaHorariaSemanalPratica)
                .HasColumnName("curdis_carga_horaria_semanal_pratica");

            builder.Property(lnq => lnq.CargaHorariaSemanalTeorica)
                .HasColumnName("curdis_carga_horaria_semanal_teoricoa");

            builder.Property(lnq => lnq.HoraAulaTotal)
                .HasColumnName("curdis_hora_aula_total");

            builder.Property(lnq => lnq.HoraTotal)
                .HasColumnName("curdis_hora_total");

            builder.Property(lnq => lnq.Credito)
                .HasColumnName("curdis_credito");

            builder.Property(lnq => lnq.PreRequisito)
                .HasColumnName("curdis_pre_requisito");

            #endregion

            builder.ToTable("curriculo_disciplina");

            #region relacionamentos

            builder.HasOne(lnq => lnq.Curriculo)
                .WithMany(lnq => lnq.Disciplinas)
                .HasForeignKey(lnq => lnq.CodigoCurriculo);

            builder.HasOne(lnq => lnq.Disciplina)
                .WithMany(lnq => lnq.CurriculoDisciplinas)
                .HasForeignKey(lnq => lnq.CodigoDisciplina);

            #endregion relacionamentos

            return this;
        }
    }
}
