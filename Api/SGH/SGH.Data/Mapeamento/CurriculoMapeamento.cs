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
                .HasColumnName("Curric_Codigo");

            builder.Property(lnq => lnq.CodigoCurso)
                .IsRequired()
                .HasColumnName("Curric_Curso");
       
            builder.Property(lnq => lnq.Ano)
                .IsRequired()
                .HasColumnName("Curric_Ano");

            builder.ToTable("Curriculo");
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
