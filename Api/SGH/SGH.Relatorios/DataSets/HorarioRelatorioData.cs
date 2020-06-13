using SGH.Relatorios.Contratos;
using System.Collections.Generic;

namespace SGH.Relatorios.DataSets
{
    public class HorarioRelatorioData : IRelatorioData
    {
        public string Semestre { get; private set; }
        public int Ano { get; private set; }
        public string Curso { get; private set; }
        public string Turno { get; private set; }
        public IList<QuadroHorario> Horarios { get; private set; }
        public IList<AulaData> Aulas { get; private set; }

        public HorarioRelatorioData(int ano, string curso, string turno,string semestre, IList<QuadroHorario> horarios, IList<AulaData> aulas)
        {
            Ano = ano;
            Curso = curso;
            Turno = turno;
            Horarios = horarios;
            Aulas = aulas;
            Semestre = semestre;
        }

    }
}
