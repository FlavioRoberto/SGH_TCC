using SGH.Relatorios.Contratos;
using System.Collections;
using System.Collections.Generic;

namespace SGH.Relatorios.DataSets
{
    public class HorarioIndividualRelatorioData : IRelatorioData
    {
        public string Semestre { get; set; }
        public int Ano { get; set; }
        public string Professor { get; set; }
        public string Cargo { get; set; }

        public IList<HorarioIndividualDisciplina> DisciplinasMinistradas { get; set; }

        public HorarioIndividualRelatorioData()
        {
            DisciplinasMinistradas = new List<HorarioIndividualDisciplina>();
        }
    }
}
