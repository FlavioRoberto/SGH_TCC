using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dominio.Model
{
    public class Curso
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        [JsonIgnore]
        public virtual List<Curriculo> Curriculos { get; set; }
    }
}
