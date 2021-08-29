using FastReport;
using SGH.Dominio.Core.DomainObjects.Datasets;

namespace SGH.Relatorios.Implementacoes
{
    internal class HorarioIndividualRelatorio : Relatorio<HorarioIndividualRelatorioData>
    {
        private readonly HorarioIndividualRelatorioData _data;

        public HorarioIndividualRelatorio(HorarioIndividualRelatorioData data) : base("Horario_Individual.frx")
        {
            _data = data;
        }

        protected override Report RegistrarDataSet(Report report)
        {
            report.RegisterData(_data.DisciplinasMinistradas, "DisciplinasMinistradas");
            report.GetDataSource("DisciplinasMinistradas").Enabled = true;

            report.RegisterData(_data.Aulas, "Aulas");
            report.GetDataSource("Aulas").Enabled = true;

            return report;
        }

        protected override Report RegistrarParametros(Report report)
        {
            report.SetParameterValue("Semestre", _data.Semestre);
            report.SetParameterValue("Ano", _data.Ano);
            report.SetParameterValue("Professor", _data.Professor);
            report.SetParameterValue("Cargo", _data.Cargo);

            return report;
        }
    }
}
