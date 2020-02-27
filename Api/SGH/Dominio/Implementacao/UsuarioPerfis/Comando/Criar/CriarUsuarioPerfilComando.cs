using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.UsuarioPerfis.Comando.Criar
{
    public class CriarUsuarioPerfilComando: IRequest<Resposta<UsuarioPerfil>>
    {
        public string Descricao { get; set; }
        public bool Administrador { get; set; }
    }
}
