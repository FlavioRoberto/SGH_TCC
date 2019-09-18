using Dominio.Contratos;

namespace Dominio.Model
{
    public class Professor : EntidadeBase
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
