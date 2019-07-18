using Dominio.Contratos;
using System.Collections.Generic;

namespace Dominio.Model.Autenticacao
{
    public class UsuarioPerfil : EntidadeBase
    {
        public string Descricao { get; set; }
        public bool Administrador { get; set; }

        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
