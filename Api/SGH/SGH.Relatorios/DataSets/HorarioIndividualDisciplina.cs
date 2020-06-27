namespace SGH.Relatorios.DataSets
{
    public class HorarioIndividualDisciplina
    {
        public string Curso { get; set; }
        public string Turno { get; set; }
        public int Periodo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeHoraTeorica  { get; set; }
        public int QuantidadeHoraPratica { get; set; }
    }
}
