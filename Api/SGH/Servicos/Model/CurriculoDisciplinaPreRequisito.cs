﻿namespace SGH.Dominio.Core.Model
{
    public class CurriculoDisciplinaPreRequisito
    {
        public int CodigoCurriculoDisciplina { get; set; }
        public int CodigoDisciplina { get; set; }

        public virtual CurriculoDisciplina CurriculoDisciplina { get; set; }
        public virtual Disciplina Disciplina { get; set; }
    }
}