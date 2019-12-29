using Dominio.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Curso : EntidadeBase
    {
        public string Descricao { get; set; }

        public virtual List<Curriculo> Curriculos { get; set; }
    }
}
