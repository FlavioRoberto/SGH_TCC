using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dominio.ViewModel.CurriculoViewModel
{
    public class CurriculoDisciplinaViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoDisciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoCurriculo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CargaHorariaSemanalTeorica { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CargaHorariaSemanalPratica { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CargaHorariaSemanalTotal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int HoraAulaTotal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int HoraTotal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Credito { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CurriculoDisciplinaPreRequisitoViewModel> PreRequisitos { get; set; }

    }
}
