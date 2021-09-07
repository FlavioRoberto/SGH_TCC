using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Comum
{
    public abstract class HorarioAulaComando
    {
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public ESemestre Semestre { get; set; }
        public EPeriodo Periodo { get; set; }
        public int CodigoTurno { get; set; }
        public int CodigoCurriculo { get; set; }
        public string Mensagem { get; set; }
    }
}
