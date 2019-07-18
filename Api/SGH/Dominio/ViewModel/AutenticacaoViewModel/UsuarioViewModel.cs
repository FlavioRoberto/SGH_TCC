using Newtonsoft.Json;

namespace Dominio.ViewModel.AutenticacaoViewModel
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
        public int PerfilCodigo { get; set; }
        public string Foto { get; set; }
    }
}
