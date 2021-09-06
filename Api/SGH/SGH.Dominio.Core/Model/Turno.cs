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
        public string Horarios { get; set; }

        public virtual IEnumerable<CargoDisciplina> DisciplinasCargo { get; set; }
        public virtual IEnumerable<Horario> HorariosAula { get; set; }

    }
}
