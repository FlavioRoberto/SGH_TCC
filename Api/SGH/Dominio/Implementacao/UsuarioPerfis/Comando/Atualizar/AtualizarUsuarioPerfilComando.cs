using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.UsuarioPerfis.Comando.Atualizar
{
    public class AtualizarUsuarioPerfilComando : IRequest<Resposta<UsuarioPerfil>>
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Administrador { get; set; }
    }
}
