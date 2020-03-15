using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover
{
    public class RemoverBlocoComando : IRequest<Resposta<bool>>
    {
        public int Codigo { get; set; }
    }
}
