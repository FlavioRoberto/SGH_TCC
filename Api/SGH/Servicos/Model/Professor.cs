using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Professor : EntidadeBase
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public IEnumerable<Cargo> Cargos { get; set; }

        public Professor()
        { }
    }
}
