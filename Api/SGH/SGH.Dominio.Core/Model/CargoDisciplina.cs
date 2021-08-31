using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class CargoDisciplina : EntidadeBase
    {
        public long CodigoCurriculoDisciplina { get; set; }
        public long CodigoCargo { get; set; }
        public long CodigoTurno { get; set; }
        public string Descricao { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual CurriculoDisciplina Disciplina { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual ICollection<Aula> Aulas { get; set; }
        public virtual ICollection<AulaDisciplinaAuxiliar> DisciplinasAuxiliar { get; set; }
    }
}