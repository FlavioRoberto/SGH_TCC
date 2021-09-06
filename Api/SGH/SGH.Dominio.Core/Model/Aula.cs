using SGH.Dominio.Core.ObjetosValor;
using System.Collections.Generic;
using System.Linq;

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
        public virtual Horario Horario { get; set; }
        public virtual CargoDisciplina Disciplina { get; set; }
        public virtual Sala Sala { get; set; }
        public virtual List<AulaDisciplinaAuxiliar> DisciplinasAuxiliar { get; private set; }

        public Aula()
        {
            this.DisciplinasAuxiliar = new List<AulaDisciplinaAuxiliar>();
        }

        public void AdicionarDisciplinaAuxiliar(List<CargoDisciplina> disciplinas)
        {
            var disciplinasAuxiliares = disciplinas.Select(lnq => new AulaDisciplinaAuxiliar(lnq.Codigo, this.Codigo));
            this.DisciplinasAuxiliar.AddRange(disciplinasAuxiliares);
        }
    }
}
