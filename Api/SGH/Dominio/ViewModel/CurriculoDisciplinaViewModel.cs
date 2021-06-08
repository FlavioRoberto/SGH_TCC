using Newtonsoft.Json;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.ViewModel
{
    public class CurriculoDisciplinaViewModel
    {
        public int? Codigo { get; set; }
        public long CodigoTipo { get; set; }

        public int Periodo { get; set; }

        public int CodigoDisciplina { get; set; }

        public int? CodigoCurriculo { get; set; }

        public int AulasSemanaisTeorica { get; set; }

        public int AulasSemanaisPratica { get; set; }

        public int QuantidadeAulaTotal { get; set; }

        public DisciplinaViewModel Disciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<DisciplinCurriculoPreRequisitoaViewModel> PreRequisitos { get; set; }

    }
}
