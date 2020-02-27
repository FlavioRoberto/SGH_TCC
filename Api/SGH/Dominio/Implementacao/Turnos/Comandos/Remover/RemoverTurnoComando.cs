using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover
{
    public class RemoverTurnoComando : IRequest<Resposta<bool>>
    {
        public int TurnoId { get; set; }
    }
}
