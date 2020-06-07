using FastReport;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.Extensions;
using SGH.Relatorios.Factories.Exportacao;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SGH.Relatorios.Implementacoes.Horario
{
    internal class HorarioRelatorio : IRelatorio<QuadroHorario>
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

            var relatorio = new Report();
 
            relatorio.Load(Path.Combine("Relatorios", "Horario.frx"));

            relatorio.RegisterData(datasetHorario, "Horarios");
            relatorio.GetDataSource("Horarios").Enabled = true;

            relatorio.SetParameterValue("Semestre", "1° Semestre");
            relatorio.SetParameterValue("Ano", "2020");
            relatorio.SetParameterValue("Curso", "Engenharia de computação");
            relatorio.SetParameterValue("Turno", "Matutino");
            relatorio.Prepare();

           // relatorio.Save(Path.Combine("Relatorios", "Horario.frx"));

            return relatorio;
        }

        private IList<QuadroHorario> GerarDataSetHorario()
        {
            return new List<QuadroHorario>() {
                new QuadroHorario {
                    Curso = "Engenharia da computação",
                    Turno = "Matutino",
                    Periodo = "1° Período",
                    Hora = "09:00",
                    DisciplinaSegunda = "Engenharia de Software \r\n (Denys Balsamão) \r\n Laboratório 301",
                    DisciplinaTerca = "POO",
                    DisciplinaQuarta = "Calculo I",
                    DisciplinaQuinta = "Sistemas Operacionais",
                    DisciplinaSexta = "",
                    DisciplinaSabado = "Sistemas Discitribuidos"                    
                },
                 new QuadroHorario {
                    Curso = "Engenharia da computação",
                    Turno = "Matutino",
                    Periodo = "1° Período",
                    Hora = "07:00",
                    DisciplinaSegunda = "",
                    DisciplinaTerca = "POO",
                    DisciplinaQuarta = "Calculo I",
                    DisciplinaQuinta = "Sistemas Operacionais",
                    DisciplinaSexta = "",
                    DisciplinaSabado = "Sistemas Discitribuidos"
                }
            };
        }
     
    }
}
