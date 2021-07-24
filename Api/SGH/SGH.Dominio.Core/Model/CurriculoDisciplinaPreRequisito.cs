namespace SGH.Dominio.Core.Model
{
    public class CurriculoDisciplinaPreRequisito
    {
        public long CodigoCurriculoDisciplina { get; set; }
        public long CodigoDisciplina { get; set; }

        public virtual CurriculoDisciplina CurriculoDisciplina { get; set; }
        public virtual Disciplina Disciplina { get; set; }
    }
}
