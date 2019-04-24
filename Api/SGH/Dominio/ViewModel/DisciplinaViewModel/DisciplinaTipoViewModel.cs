using Newtonsoft.Json;

namespace Dominio.ViewModel.DisciplinaViewModel
{
    public class DisciplinaTipoViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
