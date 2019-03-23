using System.Collections.Generic;

namespace Dominio.Model.DisciplinaModel

{
    public class DisciplinaTipo
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public virtual List<Disciplina> Disciplinas { get; set; }
    }
}
