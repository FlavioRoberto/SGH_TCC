using Dominio.Model.DisciplinaModel;
using System.Collections.Generic;

namespace Dominio.Model.CurriculoModel
{
    public class CurriculoDisciplinaPreRequisito
    {
        public int Codigo { get; set; }
        public int CodigoDisciplina { get; set; }
        public int CodigoCurriculoDisciplina { get; set; }

        public virtual Disciplina Disciplina { get; set; }
        public virtual CurriculoDisciplina CurriculoDisciplina { get; set; }
    }
}
