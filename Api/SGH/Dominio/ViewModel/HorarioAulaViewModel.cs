using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Services.ViewModel
{
    public class HorarioAulaViewModel
    {
        public int Codigo { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public EPeriodo Periodo { get; set; }
        public int CodigoTurno { get; set; }
        public int CodigoCurriculo { get; set; }
    }
}
