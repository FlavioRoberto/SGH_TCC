using FastReport;
using SGH.Relatorios.DataSets;
using System;

namespace SGH.Relatorios.Implementacoes
{
    internal class HorarioGeralRelatorio : Relatorio<HorarioGeralRelatorioData>
    {
        private readonly HorarioGeralRelatorioData _dados;

        public HorarioGeralRelatorio(HorarioGeralRelatorioData dados) : base("Horario.frx")
        {
            _dados = dados;
        }

        protected override Report RegistrarDataSet(Report relatorio)
        {
            relatorio = RegistrarDataSetAula(relatorio);
            return relatorio;
        }

        protected override Report RegistrarParametros(Report relatorio)
        {
            relatorio.SetParameterValue("Semestre", _dados.Semestre);
            relatorio.SetParameterValue("Ano", _dados.Ano);
            relatorio.SetParameterValue("Curso", _dados.Curso);
            relatorio.SetParameterValue("Turno", _dados.Turno);
            relatorio.SetParameterValue("Data", DateTime.Now.ToShortDateString());
            return relatorio;
        }

        private Report RegistrarDataSetAula(Report relatorio)
        {
            var datasetAula = _dados.Aulas;
            relatorio.RegisterData(datasetAula, "Aulas");
            relatorio.GetDataSource("Aulas").Enabled = true;

            var datasetAulaSabado = _dados.AulasSabado;
            relatorio.RegisterData(datasetAulaSabado, "AulasSabado");
            relatorio.GetDataSource("AulasSabado").Enabled = true;

            var datasetHorarios = _dados.Horarios;
            relatorio.RegisterData(datasetHorarios, "Horarios");
            relatorio.GetDataSource("Horarios").Enabled = true;

            return relatorio;
        }
    }
}
