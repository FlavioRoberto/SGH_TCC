using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class UsuarioPerfil : EntidadeBase
    {
        public string Descricao { get; set; }
        public bool Administrador { get; set; }

        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
