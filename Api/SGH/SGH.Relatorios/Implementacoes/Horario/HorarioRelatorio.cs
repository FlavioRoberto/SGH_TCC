using FastReport;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.Extensions;
using SGH.Relatorios.Factories.Exportacao;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SGH.Relatorios.Implementacoes.Horario
{
    internal class HorarioRelatorio : IRelatorio<HorarioRelatorioData>
    {
        private readonly IExportacaoFactory _exportacaoFactory;

        public HorarioRelatorio()
        {
            _exportacaoFactory = new ExportacaoRelatorioFactory();
        }

        public void Gerar()
        {
            var relatorio = GerarRelatorio();

            _exportacaoFactory.Exportar(relatorio, "Horario", ETipoExportacao.JPG);
        }

        private Report GerarRelatorio()
        {
            var datasetHorario = GerarDataSetHorario();
            var dataSetAula = GerarDataSetAula();

            var relatorio = new Report();
            relatorio.Load(Path.Combine("Relatorios", "Horario.frx"));

            relatorio.RegisterData(datasetHorario.Tables["Horarios"], "Horarios");
            relatorio.RegisterData(dataSetAula.Tables["Aulas"], "Aulas");

            relatorio.De

            relatorio.SetParameterValue("Semestre", "1° Semestre");
            relatorio.SetParameterValue("Ano", "2020");

            relatorio.Prepare();

            return relatorio;
        }

        private DataSet GerarDataSetHorario()
        {
            var lista = new List<HorarioRelatorioData>() {
                new HorarioRelatorioData {
                    Id = 1,
                    Curso = "Engenharia da computação",
                    Turno = "Matutino",
                    Periodo = "1° Período",
                }
            };
            return lista.ConverterParaDataSet("Data", "Horarios");
        }

        private DataSet GerarDataSetAula()
        {
            var lista = new List<Aula>()
            {
                new Aula
                {
                    Hora = "07:00",
                    HorarioCodigo = 1
                }
            };
            return lista.ConverterParaDataSet("Data", "Aulas");
        }
    }
}
