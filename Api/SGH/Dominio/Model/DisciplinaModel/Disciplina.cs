using Dominio.Model.CurriculoModel;
using System.Collections.Generic;

namespace Dominio.Model.DisciplinaModel
{
    public class Disciplina
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int CodigoTipo { get; set; }

        public virtual DisciplinaTipo DisciplinaTipo { get; set; }
        public virtual List<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }
        public virtual List<CurriculoDisciplina> CurriculoDisciplinas { get; set; }

    }
}
