using FastReport.Preview;
using SGH.Relatorios.Contratos;
using System.Collections.Generic;

namespace SGH.Relatorios.Implementacoes.Horario
{
    public class QuadroHorario : IRelatorioData {
        public string Curso { get; set; }
        public string Turno { get; set; }
        public string Periodo { get; set; }
        public string Hora { get; set; }
        public string DisciplinaSegunda { get; set; }
        public string SalaAulaSegunda { get; set; }
        public string ProfessorAulaSegunda { get; set; }
        public string DisciplinaTerca { get; set; }
        public string SalaAulaTerca { get; set; }
        public string ProfessorAulaTerca { get; set; }
        public string DisciplinaQuarta { get; set; }
        public string SalaAulaQuarta { get; set; }
        public string ProfessorAulaQuarta { get; set; }
        public string DisciplinaQuinta { get; set; }
        public string SalaAulaQuinta { get; set; }
        public string ProfessorAulaQuinta { get; set; }
        public string DisciplinaSexta { get; set; }
        public string SalaAulaSexta { get; set; }
        public string ProfessorAulaSexta { get; set; }
        public string DisciplinaSabado { get; set; }
        public string SalaAulaSabado { get; set; }
        public string ProfessorAulaSabado { get; set; }
    }
}
