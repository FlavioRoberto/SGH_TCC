using Dominio.Contratos;
using Dominio.Model.CurriculoModel;
using System.Collections.Generic;

namespace Dominio.Model
{
    public class Curriculo: EntidadeBase
    {
        public int Periodo { get; set; }
        public int CodigoCurso { get; set; }
        public int CodigoTurno { get; set; }
        public int Ano { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual List<CurriculoDisciplina> Disciplinas { get; set; }

    }
}
