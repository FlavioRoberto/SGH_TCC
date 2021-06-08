using System.Collections;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Sala : EntidadeBase
    {
        public int Numero { get; set; }

        public string Descricao { get; set; }

        public bool Laboratorio { get; set; }

        public long CodigoBloco { get; set; }

        public virtual Bloco Bloco { get; set; }
        public virtual ICollection<Aula> Aulas { get; set; }
    }
}
