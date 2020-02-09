namespace SGH.Dominio.Core.Model
{
    public class CargoDisciplina : EntidadeBase
    {
        public int CodigoCurriculoDisciplina { get; set; }

        public int CodigoCargo { get; set; }

        public virtual CurriculoDisciplina Disciplina { get; set; }
        public virtual Cargo Cargo { get; set; } 
    }
}