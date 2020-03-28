using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Remover
{
    public class RemoverSalaComando : IRequest<Resposta<bool>>
    {
        public int Codigo { get; set; }
    }
}
