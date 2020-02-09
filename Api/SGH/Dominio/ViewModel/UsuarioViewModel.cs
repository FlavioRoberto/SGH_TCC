using Newtonsoft.Json;

namespace SGH.Dominio.ViewModel
{
    public class UsuarioViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool Ativo { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int PerfilCodigo { get; set; }
        public string Foto { get; set; }
    }
}
