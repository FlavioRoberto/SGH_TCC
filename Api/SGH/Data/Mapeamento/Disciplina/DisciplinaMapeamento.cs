
using Dominio.Model.Disciplina;

namespace Data.Mapeamento.DisciplinaMapeamento
{
    public class DisciplinaMapeamento : EntidadeMapeamento<Disciplina>
    {
        public DisciplinaMapeamento(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Disciplina> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Disciplina> Map()
        {
            return this;
        }
    }
}
