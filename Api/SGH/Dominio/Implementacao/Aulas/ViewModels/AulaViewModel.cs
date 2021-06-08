using SGH.Dominio.Core.ObjetosValor;

namespace SGH.Dominio.Services.Implementacao.Aulas.ViewModels
{
    public class AulaViewModel
    {
        public long CodigoHorario { get; set; }
        public long Codigo { get; set; }
        public long CodigoCargo { get; set; }
        public long CodigoSala { get; set; }
        public string Professor { get; set; }
        public string Disciplina { get; set; }
        public string Sala { get; set; }
        public Reserva Reserva { get; set; }
        public string DescricaoDesdobramento { get; set; }
        public bool Laboratorio { get; set; }
        public bool HorarioExtrapolado { get; set; }

    }
}

