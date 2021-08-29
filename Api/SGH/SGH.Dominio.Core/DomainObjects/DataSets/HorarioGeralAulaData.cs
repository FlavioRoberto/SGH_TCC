namespace SGH.Dominio.Core.DomainObjects.Datasets
{
    public class HorarioGeralAulaData
    {
        public long HorarioCodigo { get; set; }
        public DisciplinaData DisciplinaSegunda { get; set; }
        public DisciplinaData DisciplinaTerca { get; set; }
        public DisciplinaData DisciplinaQuarta { get; set; }
        public DisciplinaData DisciplinaQuinta { get; set; }
        public DisciplinaData DisciplinaSexta { get; set; }
        public DisciplinaData DisciplinaSabado { get; set; }
    }
}

public class DisciplinaData
{
    public long HorarioCodigo { get; set; }
    public string Disciplina { get; set; }
    public string Hora { get; set; }
}
