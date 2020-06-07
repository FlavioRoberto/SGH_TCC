using FastReport.Preview;
using SGH.Relatorios.Contratos;
using System.Collections.Generic;

namespace SGH.Relatorios.Implementacoes.Horario
{
    public class QuadroHorario : IRelatorioData {
        public int Codigo { get; set; }
        public string Periodo { get; set; }
    }

    public class Aula
    {
        public int HorarioCodigo { get; set; }
        public string Hora { get; set; }
        public string DisciplinaSegunda { get; set; }
        public string DisciplinaTerca { get; set; }
        public string DisciplinaQuarta { get; set; }
        public string DisciplinaQuinta { get; set; }
        public string DisciplinaSexta { get; set; }
        public string DisciplinaSabado { get; set; }
    }
}
