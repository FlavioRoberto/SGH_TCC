using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class CurriculoDisciplina: EntidadeBase
    {
        public int? CodigoDisciplina { get; set; }
        public int CodigoCurriculo { get; set; }
        public int Periodo { get; set; }
        public int AulasSemanaisTeorica { get; set; }
        public int AulasSemanaisPratica { get; set; }
        public int Credito { get; set; }

        public virtual Disciplina Disciplina { get; set; }
        public virtual Curriculo Curriculo { get; set; }
        public virtual IEnumerable<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }
        public virtual IEnumerable<CargoDisciplina> Cargos { get; set; }
    }
}
