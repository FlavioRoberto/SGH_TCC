using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Turno : EntidadeBase
    {
        public Turno()
        {
            Codigo = 0;
            Descricao = "";
        }

        public string Descricao { get; set; }

        public virtual List<CargoDisciplina> DisciplinasCargo { get; set; }
    }
}
