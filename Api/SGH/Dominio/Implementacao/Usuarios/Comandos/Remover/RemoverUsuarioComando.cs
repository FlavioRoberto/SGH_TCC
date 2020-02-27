using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Remover
{
    public class RemoverUsuarioComando : IRequest<Resposta<bool>>
    {
        public int CodigoUsuario { get; set; }
    }
}
