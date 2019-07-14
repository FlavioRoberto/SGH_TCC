namespace Dominio.ViewModel.CurriculoViewModel
{
    public class CurriculoDisciplinaViewModel
    {
        public int Codigo { get; set; }
        public int CodigoDisciplina { get; set; }
        public int CodigoCurriculo { get; set; }
        public int CargaHorariaSemanalTeorica { get; set; }
        public int CargaHorariaSemanalPratica { get; set; }
        public int CargaHorariaSemanalTotal { get; set; }
        public int HoraAulaTotal { get; set; }
        public int HoraTotal { get; set; }
        public int Credito { get; set; }
        public bool PreRequisito { get; set; }

    }
}
