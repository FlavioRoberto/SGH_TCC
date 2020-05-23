using SGH.Relatorios.Implementacoes;
using SGH.Relatorios.Implementacoes.Horario;
using System;

namespace SGH.Relatorios
{
    class Program
    {
        static void Main(string[] args)
        {
            new HorarioRelatorio().Gerar();
            Console.WriteLine("Hello World!");
        }
    }
}
