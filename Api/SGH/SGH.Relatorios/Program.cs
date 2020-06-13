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
            var horarios = new List<QuadroHorario> {
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
            var aulas = new List<AulaData> {
             new AulaData
               {
                   HorarioCodigo = 1,
                   Hora = "07:00",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 1,
                   Hora = "07:50",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 1,
                   Hora = "08:40",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 1,
                   Hora = "09:45",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 1,
                   Hora = "10:35",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
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
                new AulaData
               {
                   HorarioCodigo = 2,
                   Hora = "07:00",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 2,
                   Hora = "07:50",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 2,
                   Hora = "08:40",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 2,
                   Hora = "09:30",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 2,
                   Hora = "09:45",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
               {
                   HorarioCodigo = 2,
                   Hora = "10:35",
                   DisciplinaSegunda = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 511",
                   DisciplinaTerca = "Programação de Computador \r\n Turma A \r\n (Cargo 86) \r\n Laboratório A1",
                   DisciplinaQuarta = "Quimica Geral \r\n (Viviane Aparecida Costa) \r\n Sala 512",
                   DisciplinaQuinta = "Quimica Geral \r\n Prática - B \r\n (Viviane Aparecida Costa)",
                   DisciplinaSexta = "Introdução a Engenharia Civil \r\n (Osvaldo Sena Guimarães) \r\n Sala 501"
               },
               new AulaData
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

            var relatorio = new RelatorioServico().GerarRelatorioHorario(new HorarioRelatorioData(2020, "Engenharia Civil", "Matutino","1° Semestre", horarios, aulas));

            File.WriteAllBytes(Path.Combine("Relatorios", "Horario.pdf"), relatorio);

            Console.WriteLine("Hello World!");
        }
    }
}
