using Dominio.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dominio.ViewModel
{
    public class ProfessorViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<int> Cursos { get; set; }

        public ProfessorViewModel()
        {
            Cursos = new List<int>();
        }
    }
}
