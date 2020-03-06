using Newtonsoft.Json;
using System.Collections.Generic;

namespace SGH.Dominio.ViewModel
{
    public class CurriculoDisciplinaViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }

        public int Periodo { get; set; }

        public int CodigoDisciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoCurriculo { get; set; }

        public int AulasSemanaisTeorica { get; set; }

        public int AulasSemanaisPratica { get; set; }

        public DisciplinaViewModel Disciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<DisciplinaViewModel> PreRequisitos { get; set; }

    }
}
