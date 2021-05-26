using SGH.Relatorios.DataSets;
using SGH.Relatorios.Implementacoes;
using System;
using System.Collections.Generic;
using System.IO;

namespace SGH.Relatorios
{
    class Program
    {
        static void Main(string[] args)
        {
            var relatorio = GerarRelatorioGeral();

            File.WriteAllBytes(Path.Combine("Relatorios", "Horario.pdf"), relatorio);

        //    relatorio = GerarRelatorioIndividual();

        //    File.WriteAllBytes(Path.Combine("Relatorios", "Horario.pdf"), relatorio);

            Console.WriteLine("Hello World!");
        }

        private static byte[] GerarRelatorioIndividual()
        {
            var dados = new HorarioIndividualRelatorioData
            {
                Ano = 2020,
                Cargo = "Cargo 56 Edital 025/2020",
                Professor = "Flávio Roberto Teixeira",
                Semestre = "1° Semestre",
                DisciplinasMinistradas = new[]
                {
                    new HorarioIndividualDisciplinaData
                    {
                        Curso = "Engenharia da computação",
                        Descricao = "Programação Orientada a Objetos",
                        Periodo = 5,
                        QuantidadeHoraPratica = 8,
                        QuantidadeHoraTeorica = 10,
                        Turno = "Matutino"
                    },
                     new HorarioIndividualDisciplinaData
                    {
                        Curso = "Engenharia da computação",
                        Descricao = "Arquitetura de computadores",
                        Periodo = 5,
                        QuantidadeHoraPratica = 4,
                        QuantidadeHoraTeorica = 8,
                        Turno = "Noturno"
                    }
                },
                Aulas = new[]
                {
                    new HorarioIndividualAulasData
                    {
                        Turno = "Matutino",
                        Hora = "07:00",
                        DisciplinaQuarta = $"(5° Período) {Environment.NewLine} Arquitetura de computadores"
                    },
                    new HorarioIndividualAulasData
                    {
                        Turno = "Noturno",
                        Hora = "19:00",
                        DisciplinaQuarta = $"(5° Período) {Environment.NewLine} Arquitetura de computadores"
                    },
                    new HorarioIndividualAulasData
                    {
                        Turno = "Matutino",
                        Hora = "09:00",
                        DisciplinaSegunda = $"(5° Período) {Environment.NewLine} Arquitetura de computadores"
                    },
                    new HorarioIndividualAulasData
                    {
                        Turno = "Noturno",
                        Hora = "19:00",
                        DisciplinaQuarta = $"(5° Período) {Environment.NewLine} Arquitetura de computadores"
                    },
                    new HorarioIndividualAulasData
                    {
                        Turno = "Vespertino",
                        Hora = "14:00",
                        DisciplinaQuinta = $"(5° Período) {Environment.NewLine} Programação orientada a objetos"
                    },  new HorarioIndividualAulasData
                    {
                        Turno = "Vespertino",
                        Hora = "14:00",
                        DisciplinaTerca = $"(5° Período) {Environment.NewLine} Engenharia de software"
                    }
                }
            };
            return new RelatorioServico().GerarRelatorioHorarioIndividual(dados);
        }

        private static byte[] GerarRelatorioGeral()
        {
            var horarios = new List<QuadroHorarioData> {
             new QuadroHorarioData {
                    Codigo = 1,
                    Periodo = "1° Período",
                    Avisos = "Introdução a Engenharia Civil: 2 Aulas de 15 em 15 dias - Março: 01, 15 e 29 - Abril: 12 e 26 - Maio: 10 e 24 - Junho: 26 - Julho: 05"
                },
                new QuadroHorarioData
                {
                    Codigo = 2,
                    Periodo = "2° Período"
                }
            };
            var aulas = new List<HorarioGeralAulaData> {
             new HorarioGeralAulaData
               {
                   HorarioCodigo = 1,
                   DisciplinaSegunda = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "18:30" },
                   DisciplinaTerca = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "18:30" },
                   DisciplinaQuarta =new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "18:30" },
                   DisciplinaQuinta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "18:30" },
                   DisciplinaSexta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "18:30" },
                   DisciplinaSabado = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "07:00" }
               },
                new HorarioGeralAulaData {
                   HorarioCodigo = 1,
                   DisciplinaSegunda = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "19:20" },
                   DisciplinaTerca = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "19:20" },
                   DisciplinaQuarta =new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "19:20" },
                   DisciplinaQuinta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "19:20" },
                   DisciplinaSexta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "19:20" },
                   DisciplinaSabado = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "07:50" }
               },
                 new HorarioGeralAulaData {
                   HorarioCodigo = 1,
                   DisciplinaSegunda = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "20:25" },
                   DisciplinaTerca = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "20:25" },
                   DisciplinaQuarta =new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "20:25" },
                   DisciplinaQuinta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "20:25" },
                   DisciplinaSexta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "20:25" },
                   DisciplinaSabado = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "09:45" }
               },
                  new HorarioGeralAulaData {
                   HorarioCodigo = 1,
                   DisciplinaSegunda = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "21:25" },
                   DisciplinaTerca = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "21:25" },
                   DisciplinaQuarta =new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "21:25" },
                   DisciplinaQuinta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "21:25" },
                   DisciplinaSexta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "21:25" },
                   DisciplinaSabado = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "10:45" }
               },
                    new HorarioGeralAulaData {
                   HorarioCodigo = 1,
                   DisciplinaSegunda = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "22:25" },
                   DisciplinaTerca = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "22:25" },
                   DisciplinaQuarta =new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "22:25" },
                   DisciplinaQuinta = new DisciplinaData{ Disciplina = "-", Hora = "-" },
                   DisciplinaSexta = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "22:25" },
                   DisciplinaSabado = new DisciplinaData{ Disciplina = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511", Hora = "11:45" }
               }
            };

            return new RelatorioServico().GerarRelatorioHorarioGeral(new HorarioGeralRelatorioData(2020, "Engenharia Civil", "Matutino", "1° Semestre", horarios, aulas));
        }
    }
}
