namespace SGH.Dominio.Events
{
    public class EmailEnviadoEvent
    {
        public string Email { get; private set; }
        public string Assunto { get; private set; }
        public string Mensagem { get; private set; }
    }
}
