using SGH.Dominio.Core.Model;

namespace SGH.Dominio.ViewModel
{
    public class CargoDisciplinaViewModel
    {
        public int? Codigo { get; set; }
        public int? CodigoCargo { get; set; }
        public int? CodigoCurriculoDisciplina { get; set; }
        public string CursoDescricao { get; set; }
        public string Descricao { get; set; }
        public string TurnoDescricao { get; set; }
    }
}
