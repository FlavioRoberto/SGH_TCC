namespace Api.Servicos.Email
{
    public class EmailSettings
    {
        public string Dominio { get; set; }
        public int Porta { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CcoEmail { get; set; }
    }
}
