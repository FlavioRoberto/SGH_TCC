using SGH.Dominio.Core.Model;

namespace SGH.Dominio.ViewModel
{
    public class CargoDisciplinaViewModel
    {
        public int? Codigo { get; set; }
        public int? CodigoCargo { get; set; }
        public int? CodigoCurriculoDisciplina { get; set; }
        public  string disciplinaDescricao { get; set; }
        public string cursoDescricao { get; set; }
        public string turnoDescricao { get; set; }
    }
}
