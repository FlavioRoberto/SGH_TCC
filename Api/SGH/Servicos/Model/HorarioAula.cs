using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Core.Model
{
    public class HorarioAula : EntidadeBase
    {
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public EPeriodo Periodo { get; set; }
        public int CodigoTurno { get; set; }
        public int CodigoCurriculo { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Curriculo Curriculo { get; set; }
    }
}
