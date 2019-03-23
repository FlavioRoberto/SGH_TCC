using Dominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento.CurriculoMapeamento
{
    public class CurriculoMapeamento : EntidadeMapeamento<Curriculo>
    {
        public CurriculoMapeamento(EntityTypeBuilder<Curriculo> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Curriculo> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            #region Properties

            builder.Property(lnq => lnq.Codigo)
                .ValueGeneratedOnAdd()
                .HasColumnName("curric_codigo");

            builder.Property(lnq => lnq.Periodo)
                .IsRequired()
                .HasColumnName("curric_periodo");

            builder.Property(lnq => lnq.CodigoCurso)
                .IsRequired()
                .HasColumnName("curric_curso");

            builder.Property(lnq => lnq.CodigoTurno)
                .IsRequired()
                .HasColumnName("curric_turno");


            builder.Property(lnq => lnq.Ano)
                .IsRequired()
                .HasColumnName("curric_ano");

            builder.ToTable("curriculo");
            #endregion

            #region relacionamentos

            builder.HasOne(lnq => lnq.Curso)
                .WithMany(lnq => lnq.Curriculos)
                .HasForeignKey(lnq => lnq.CodigoCurso);

            builder.HasOne(lnq => lnq.Turno)
               .WithMany(lnq => lnq.Curriculos)
               .HasForeignKey(lnq => lnq.CodigoTurno);

            #endregion

            return this;
        }
    }
}
