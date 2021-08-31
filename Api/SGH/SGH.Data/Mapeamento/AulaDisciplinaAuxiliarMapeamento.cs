using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;
using SHG.Data.Mapeamento;

namespace SGH.Data.Mapeamento
{
    public class AulaDisciplinaAuxiliarMapeamento : EntidadeMapeamento<AulaDisciplinaAuxiliar>
    {
        protected override string PrefixoColuna { get => "AulaDiscAux"; }

        public AulaDisciplinaAuxiliarMapeamento(EntityTypeBuilder<AulaDisciplinaAuxiliar> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<AulaDisciplinaAuxiliar> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo)
                   .HasColumnName(RetornarNomeColuna("Codigo"));

            builder.Property(lnq => lnq.CodigoAula)
                    .HasColumnName(RetornarNomeColuna("Aula"));

            builder.Property(lnq => lnq.CodigoCargoDisciplina)
                    .HasColumnName(RetornarNomeColuna("Disciplina"));

            builder.HasOne(lnq => lnq.Aula)
                    .WithMany(lnq => lnq.DisciplinasAuxiliar)
                    .HasForeignKey(lnq => lnq.CodigoAula)
                    .HasConstraintName("FK_Aula");

            builder.HasOne(lnq => lnq.Disciplina)
                   .WithMany(lnq => lnq.DisciplinasAuxiliar)
                   .HasForeignKey(lnq => lnq.CodigoCargoDisciplina)
                   .HasConstraintName("FK_Disciplina");

            builder.ToTable("AulaDisciplinaAuxiliar");

            return this;
        }


    }
}
