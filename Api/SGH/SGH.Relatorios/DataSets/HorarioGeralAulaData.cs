namespace SGH.Relatorios.DataSets
{
    public class HorarioGeralAulaData
    {
        public int HorarioCodigo { get; set; }
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
    public string Disciplina { get; set; }
    public string Hora { get; set; }
}
