using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Bloco : EntidadeBase
    {
        public string Descricao { get; set; }
        public virtual ICollection<Sala> Salas { get; set; }
    }
}
