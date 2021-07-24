using SGH.Dominio.Core.Enums;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class CurriculoDisciplina: EntidadeBase
    {
        public long? CodigoDisciplina { get; set; }
        public long CodigoCurriculo { get; set; }
        public long CodigoTipo { get; set; }
        public EPeriodo Periodo { get; set; }
        public int AulasSemanaisTeorica { get; set; }
        public int AulasSemanaisPratica { get; set; }
        public int QuantidadeAulaTotal { get; set; }

        public virtual Disciplina Disciplina { get; set; }
        public virtual DisciplinaTipo DisciplinaTipo { get; set; }
        public virtual Curriculo Curriculo { get; set; }
        public virtual IEnumerable<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }
        public virtual IEnumerable<CargoDisciplina> Cargos { get; set; }
    }
}
