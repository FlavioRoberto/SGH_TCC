namespace Dominio.Model.Autenticacao
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public int PerfilCodigo { get; set; }
        public string Foto { get; set; }

        public UsuarioPerfil Perfil { get; set; }
    }
}
