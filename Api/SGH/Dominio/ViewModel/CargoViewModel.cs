using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.ViewModel
{
    public class CargoViewModel
    {
        public int? Codigo { get; set; }
        public int? Numero { get; set; }
        public string Edital { get; set; }
        public int? Ano { get; set; }
        public ESemestre? Semestre { get; set; }
        public int? CodigoProfessor { get; set; }
    }
}
