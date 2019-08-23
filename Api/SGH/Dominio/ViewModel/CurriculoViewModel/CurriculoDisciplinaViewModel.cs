using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dominio.ViewModel.CurriculoViewModel
{
    public class CurriculoDisciplinaViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }

        public int CodigoDisciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CodigoCurriculo { get; set; }

        public int CargaHorariaSemanalTeorica { get; set; }

        public int CargaHorariaSemanalPratica { get; set; }

        public int CargaHorariaSemanalTotal { get; set; }

        public int HoraAulaTotal { get; set; }

        public int HoraTotal { get; set; }

        public int Credito { get; set; }

        public DisciplinaViewModel.DisciplinaViewModel Disciplina { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<DisciplinaViewModel.DisciplinaViewModel> PreRequisitos { get; set; }

    }
}
