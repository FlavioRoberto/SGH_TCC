using SGH.Dominio.Core.ObjetosValor;

namespace SGH.Dominio.Core.Model
{
    public class Aula : EntidadeBase
    {
        public long CodigoHorario { get; set; }
        public long CodigoDisciplina { get; set; }
        public long? CodigoSala { get; set; }
        public bool Laboratorio { get; set; }
        public bool Desdobramento { get; set; }
        public string DescricaoDesdobramento { get; set; }
        public Reserva Reserva { get; set; }
        public virtual HorarioAula Horario { get; set; }
        public virtual CargoDisciplina Disciplina { get; set; }
        public virtual Sala Sala { get; set; }
    }
}
