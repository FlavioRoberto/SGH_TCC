using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Curso : EntidadeBase
    {
        public string Descricao { get; set; }

        public virtual ICollection<Curriculo> Curriculos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}
