using SGH.Relatorios.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace SGH.Relatorios.DataSets
{
    public class HorarioGeralRelatorioData : IRelatorioData
    {
        public string Semestre { get; private set; }
        public int Ano { get; private set; }
        public string Curso { get; private set; }
        public string Turno { get; private set; }
        public IList<QuadroHorarioData> Horarios { get; private set; }
        public IList<HorarioGeralAulaData> Aulas { get; private set; }
        public IList<DisciplinaData> AulasSabado { get; private set; }


        public HorarioGeralRelatorioData(int ano, string curso, string turno,string semestre, IList<QuadroHorarioData> horarios, IList<HorarioGeralAulaData> aulas, IList<DisciplinaData> aulasSabado)
        {
            Ano = ano;
            Curso = curso;
            Turno = turno;
            Horarios = horarios;
            Aulas = aulas;
            AulasSabado = aulasSabado;
            Semestre = semestre;
        }

    }
}
