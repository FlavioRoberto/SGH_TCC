using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
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

            builder.Property(lnq => lnq.CodigoCurso)
                .IsRequired()
                .HasColumnName("curric_curso");
       
            builder.Property(lnq => lnq.Ano)
                .IsRequired()
                .HasColumnName("curric_ano");

            builder.ToTable("curriculo");
            #endregion

            #region relacionamentos

            builder.HasOne(lnq => lnq.Curso)
                .WithMany(lnq => lnq.Curriculos)
                .HasForeignKey(lnq => lnq.CodigoCurso)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            return this;
        }
    }
}
