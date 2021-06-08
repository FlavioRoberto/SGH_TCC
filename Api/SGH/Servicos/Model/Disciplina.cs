using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Disciplina : EntidadeBase
    {
        public string Descricao { get; set; }
        public virtual IEnumerable<CurriculoDisciplina> CurriculoDisciplinas { get; set; }
        public virtual IEnumerable<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }

    }
}
