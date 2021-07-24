
using Microsoft.EntityFrameworkCore;
using SGH.Dominio.Core.Model;

namespace SHG.Data.Mapeamento
{
    public class DisciplinaMapeamento : EntidadeMapeamento<Disciplina>
    {
        public DisciplinaMapeamento(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Disciplina> builder) : base(builder)
        {
           
        }

        public override EntidadeMapeamento<Disciplina> Map()
        {
            builder.HasKey(lnq => lnq.Codigo);

            builder.Property(lnq => lnq.Codigo).HasColumnName("Dis_Codigo");
            builder.Property(lnq => lnq.Descricao).HasColumnName("Dis_Descricao");
            builder.ToTable("Disciplina");
            return this;
        }
    }
}
