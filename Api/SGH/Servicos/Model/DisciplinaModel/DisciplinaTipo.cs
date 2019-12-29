using Dominio.Model.DisciplinaModel;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model

{
    public class DisciplinaTipo : EntidadeBase
    {
        public string Descricao { get; set; }

        public virtual List<Disciplina> Disciplinas { get; set; }
    }
}
