﻿using Dominio.Model.Disciplina;
using System.Collections.Generic;

namespace Dominio.Model
{
    public class Curriculo
    {
        public int Codigo { get; set; }
        public int Periodo { get; set; }
        public int CodigoCurso { get; set; }
        public int CodigoTurno { get; set; }
        public int Ano { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual List<DisciplinaCurriculo> Disciplinas { get; set; }

    }
}
