using Dominio.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SGH.APi.ViewModel
{
    public class ProfessorViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool Ativo { get; set; }

        public ProfessorViewModel()
        { }
    }
}
