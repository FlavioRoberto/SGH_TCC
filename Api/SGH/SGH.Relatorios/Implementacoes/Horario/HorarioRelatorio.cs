using FastReport;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.Extensions;
using SGH.Relatorios.Factories.Exportacao;
using System;
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
            _exportacaoFactory.Exportar(relatorio, "Horario", ETipoExportacao.PDF);
        }

        private Report GerarRelatorio()
        {
            var datasetHorario = GerarDataSetHorario();
            var datasetAula = GerarDataSetAula();

            var relatorio = new Report();
 
            relatorio.Load(Path.Combine("Relatorios", "Horario.frx"));

            relatorio.RegisterData(datasetHorario, "Horarios");
            relatorio.GetDataSource("Horarios").Enabled = true;

            relatorio.RegisterData(datasetAula, "Aulas");
            relatorio.GetDataSource("Aulas").Enabled = true;

            relatorio.SetParameterValue("Semestre", "1° Semestre");
            relatorio.SetParameterValue("Ano", "2020");
            relatorio.SetParameterValue("Curso", "Engenharia Civil");
            relatorio.SetParameterValue("Turno", "Matutino");
            relatorio.SetParameterValue("Data", DateTime.Now.ToShortDateString());

            relatorio.Prepare();

            relatorio.Save(Path.Combine("Relatorios", "Horario.frx"));

            return relatorio;
        }

        private IList<QuadroHorario> GerarDataSetHorario()
        {
            return new List<QuadroHorario>() {
                new QuadroHorario {
                    Codigo = 1,
                    Periodo = "1° Período",
                    Avisos = "Introdução a Engenharia Civil: 2 Aulas de 15 em 15 dias - Março: 01, 15 e 29 - Abril: 12 e 26 - Maio: 10 e 24 - Junho: 26 - Julho: 05"
                },
                new QuadroHorario
                {
                    Codigo = 2,
                    Periodo = "2° Período"
                }
            };
        }

        private IList<Aula> GerarDataSetAula()
        {
            return new List<Aula> {
               new Aula
               {
                   HorarioCodigo = 1,
                   Hora = "07:00",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 1,
                   Hora = "07:50",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 1,
                   Hora = "08:40",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 1,
                   Hora = "09:45",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 1,
                   Hora = "10:35",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 1,
                   Hora = "11:25",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               //
                new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "07:00",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "07:50",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "08:40",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "09:30",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "09:45",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "10:35",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new Aula
               {
                   HorarioCodigo = 2,
                   Hora = "11:25",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               }
            };
        }

    }
}
