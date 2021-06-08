using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Curriculo: EntidadeBase
    {
        public long CodigoCurso { get; set; }
        public long Ano { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual IEnumerable<CurriculoDisciplina> Disciplinas { get; set; }
        public virtual IEnumerable<HorarioAula> HorariosAula { get; set; }        
    }
}
