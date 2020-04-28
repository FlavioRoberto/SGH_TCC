using SGH.Dominio.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public class CursoMapeamento : EntidadeMapeamento<Curso>
    {
        public CursoMapeamento(EntityTypeBuilder<Curso> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Curso> Map()
        {

            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo)
                .HasColumnName("Curso_Codigo")
                .ValueGeneratedOnAdd();

            builder.Property(lnq => lnq.Descricao)
                .HasColumnName("Curso_Descricao")
                .IsRequired();

            builder.ToTable("Curso");


            return this;
        }
    }
}
