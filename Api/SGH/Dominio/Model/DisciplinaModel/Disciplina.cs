namespace Dominio.Model.Disciplina
{
    public class Disciplina
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int CodigoTipo { get; set; }

        public virtual DisciplinaTipo DisciplinaTipo { get; set; }
    }
}
