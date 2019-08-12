using Dominio.Contratos;
using Dominio.Model.CurriculoModel;
using System.Collections.Generic;

namespace Dominio.Model.DisciplinaModel 
{
    public class Disciplina : EntidadeBase
    {
        public string Descricao { get; set; }
        public int CodigoTipo { get; set; }

        public virtual DisciplinaTipo DisciplinaTipo { get; set; }
        public virtual IEnumerable<CurriculoDisciplina> CurriculoDisciplinas { get; set; }
        public virtual IEnumerable<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }

    }
}
