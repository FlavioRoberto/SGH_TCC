using SGH.Dominio.Core.Enums;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Cargo : EntidadeBase
    {
        public int Numero { get; set; }
        public int Edital { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public int? CodigoProfessor { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual IEnumerable<CargoDisciplina> Disciplinas { get; set; }

    }
}
