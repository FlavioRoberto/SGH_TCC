﻿using SGH.Dominio.Core.Enums;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Cargo : EntidadeBase
    {
        public int Numero { get; set; }
        public string Edital { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public long? CodigoProfessor { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual IEnumerable<CargoDisciplina> Disciplinas { get; set; }

        public string RetornarDescricao()
        {
            return Professor?.Nome ?? $"Cargo {Numero}";
        }
    }
}
