using Newtonsoft.Json;

namespace Dominio.ViewModel.CurriculoViewModel
{
    public class CurriculoDisciplinaPreRequisitoViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoCurriculoDisciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoDisciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CurriculoDisciplinaViewModel Disciplina { get; set; }
    }
}
