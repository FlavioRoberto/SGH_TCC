using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class CargoDisciplina : EntidadeBase
    {
        public int CodigoCurriculoDisciplina { get; set; }
        public int CodigoCargo { get; set; }
        public int CodigoTurno { get; set; }
        public string Descricao { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual CurriculoDisciplina Disciplina { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual ICollection<Aula> Aulas { get; set; }
    }
}