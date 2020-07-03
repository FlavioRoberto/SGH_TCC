using Newtonsoft.Json;
using System.Collections.Generic;

namespace SGH.Dominio.ViewModel
{
    public class TurnoViewModel
    {
        public int? Codigo { get; set; }
        public string Descricao { get; set; }
        public string[] Horarios { get; set; }
    }
}
