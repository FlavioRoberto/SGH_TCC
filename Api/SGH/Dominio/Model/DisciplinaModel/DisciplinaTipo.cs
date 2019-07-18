using Dominio.Contratos;
using System.Collections.Generic;

namespace Dominio.Model.DisciplinaModel

{
    public class DisciplinaTipo : EntidadeBase
    {
        public string Descricao { get; set; }

        public virtual List<Disciplina> Disciplinas { get; set; }
    }
}
