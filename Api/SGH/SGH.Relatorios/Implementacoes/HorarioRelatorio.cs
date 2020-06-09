using FastReport;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.Factories.Exportacao;
using SGH.Relatorios.DataSets;
using System;
using System.IO;

namespace SGH.Relatorios.Implementacoes
{
    internal class HorarioRelatorio : IRelatorio<HorarioRelatorioData>
    {
        private readonly IExportacaoFactory _exportacaoFactory;
        private readonly HorarioRelatorioData _dados;

        public HorarioRelatorio(HorarioRelatorioData dados)
        {
            _exportacaoFactory = new ExportacaoRelatorioFactory();
            _dados = dados;
        }

        public byte[] Gerar()
        {
            var relatorio = GerarRelatorio();
            return _exportacaoFactory.Exportar(relatorio, "Horario", ETipoExportacao.PDF);
        }

        private Report GerarRelatorio()
        {
            var relatorio = new Report();

            relatorio = RegistrarDataSet(relatorio);

            relatorio.Load(Path.Combine("Relatorios", "Horario.frx"));

            relatorio = DefinirParametros(relatorio);

            relatorio.Prepare();

            return relatorio;
        }

        private Report RegistrarDataSet(Report relatorio)
        {
            relatorio = RegistrarDataSetHorario(relatorio);
            relatorio = RegistrarDataSetAula(relatorio);
            return relatorio;
        }

        private Report DefinirParametros(Report relatorio)
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
            return relatorio;
        }

        private Report RegistrarDataSetHorario(Report relatorio)
        {
            var datasetHorario = _dados.Horarios;
            relatorio.RegisterData(datasetHorario, "Horarios");
            relatorio.GetDataSource("Horarios").Enabled = true;
            return relatorio;
        }
    }
}
