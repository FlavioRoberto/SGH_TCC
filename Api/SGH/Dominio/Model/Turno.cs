using Dominio.Contratos;
using System.Collections.Generic;

namespace Dominio.Model
{
    public class Turno : EntidadeBase
    {
        public Turno()
        {
            Codigo = 0;
            Descricao = "";
        }

        public string Descricao { get; set; }

        public virtual List<Curriculo> Curriculos { get; set; }
    }
}
