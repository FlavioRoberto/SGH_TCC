using Newtonsoft.Json;

namespace SGH.APi.ViewModel
{
    public class TurnoViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
