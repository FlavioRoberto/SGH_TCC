using Dominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento
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
                .HasColumnName("curso_codigo")
                .ValueGeneratedOnAdd();

            builder.Property(lnq => lnq.Descricao)
                .HasColumnName("curso_descricao")
                .IsRequired();

            builder.ToTable("curso");


            return this;
        }
    }
}
