using SGH.Dominio.Core.ObjetosValor;

namespace SGH.Dominio.Services.Implementacao.Aulas.ViewModels
{
    public class AulaViewModel
    {
        public int Codigo { get; set; }
        public int CodigoCargo { get; set; }
        public int CodigoSala { get; set; }
        public string Professor { get; set; }
        public string Disciplina { get; set; }
        public string Sala { get; set; }
        public Reserva Reserva { get; set; }
        public string DescricaoDesdobramento { get; set; }
        public bool Pratica { get; set; }
        public bool HorarioExtrapolado { get; set; }

    }
}

