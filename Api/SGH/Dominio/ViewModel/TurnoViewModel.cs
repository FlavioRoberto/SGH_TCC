using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ViewModel
{
    public class TurnoViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
