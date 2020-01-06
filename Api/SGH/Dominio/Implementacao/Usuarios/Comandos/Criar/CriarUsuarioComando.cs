using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComando : IRequest<Resposta<Usuario>>
    {
    }
}
