namespace SGH.Dominio.Core.ObjetosValor
{
    public class Reserva
    {
        public string DiaSemana { get; private set; }
        public string Hora { get; private set; }

        public Reserva(string diaSemana, string hora)
        {
            DiaSemana = diaSemana;
            Hora = hora;
        }
    }
}
