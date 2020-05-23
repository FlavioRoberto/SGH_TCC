using SGH.Relatorios.Contratos;
using System.Collections.Generic;

namespace SGH.Relatorios.Implementacoes.Horario
{
    internal class Aula
    {
        public int HorarioCodigo { get; set; }
        public string Hora { get; set; }
    }

    internal class HorarioRelatorioData : IRelatorioData
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public string Turno { get; set; }
        public string Periodo { get; set; }
    }
}
