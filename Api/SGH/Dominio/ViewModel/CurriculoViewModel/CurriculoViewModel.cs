using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dominio.ViewModel.CurriculoViewModel
{
    public class CurriculoViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoCurso { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Ano { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<CurriculoDisciplinaViewModel> Disciplinas { get; set; }

        public string DescricaoCurso { get; set; }
    }
}
