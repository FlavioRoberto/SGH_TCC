using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGH.APi.ViewModel
{
    public class CursoViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
