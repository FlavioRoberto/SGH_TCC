using System.Collections.Generic;

namespace SGH.Dominio.Core.Model

{
    public class DisciplinaTipo : EntidadeBase
    {
        public string Descricao { get; set; }

        public virtual List<CurriculoDisciplina> Disciplinas { get; set; }
    }
}
