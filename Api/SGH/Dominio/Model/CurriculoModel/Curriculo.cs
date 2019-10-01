﻿using Dominio.Contratos;
using Dominio.Model.CurriculoModel;
using System.Collections.Generic;

namespace Dominio.Model
{
    public class Curriculo: EntidadeBase
    {
        public int CodigoCurso { get; set; }
        public int Ano { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual IEnumerable<CurriculoDisciplina> Disciplinas { get; set; }

    }
}
