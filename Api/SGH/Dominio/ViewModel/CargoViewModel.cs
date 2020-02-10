using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.ViewModel
{
    public class CargoViewModel
    {
        public int? Codigo { get; set; }
        public int? Numero { get; set; }
        public int? Edital { get; set; }
        public int? Ano { get; set; }
        public ESemestre? Semestre { get; set; }
        public int? CodigoProfessor { get; set; }
        public virtual IEnumerable<CargoDisciplinaViewModel> Disciplinas { get; set; }
    }
}
