using Dominio.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento
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
                .HasField("curric_codigo");

            builder.Property(lnq => lnq.Periodo)
                .IsRequired()
                .HasField("curric_periodo");

            builder.Property(lnq => lnq.CodigoCurso)
                .IsRequired()
                .HasField("curric_curso");

            builder.Property(lnq => lnq.CodigoTurno)
                .IsRequired()
                .HasField("curric_turno");


            builder.Property(lnq => lnq.Ano)
                .IsRequired()
                .HasField("curric_ano");

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
