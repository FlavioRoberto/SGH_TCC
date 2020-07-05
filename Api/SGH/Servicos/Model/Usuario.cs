
namespace SGH.Dominio.Core.Model
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public bool Ativo { get; set; }
        public int PerfilCodigo { get; set; }
        public int? CursoCodigo { get; set; }

        public UsuarioPerfil Perfil { get; set; }
        public Curso Curso { get; set; }
    }
}
