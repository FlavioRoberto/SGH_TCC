using Newtonsoft.Json;

namespace SGH.Dominio.ViewModel
{
    public class DisciplinaViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoTipo { get; set; }
    }
}
