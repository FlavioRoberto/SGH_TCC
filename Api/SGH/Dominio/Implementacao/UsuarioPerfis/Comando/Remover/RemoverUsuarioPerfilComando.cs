using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.UsuarioPerfis.Comando.Remover
{
    public class RemoverUsuarioPerfilComando : IRequest<Resposta<bool>>
    {
        public int Codigo { get; set; }
    }
}
