using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.DefinirSala
{
    public class DefinirSalaComando : IRequest<Resposta<bool>>
    {
        public long AulaId { get; private set; }
        public long SalaId { get; private set; }

        public DefinirSalaComando(long aulaId, long salaId)
        {
            AulaId = aulaId;
            SalaId = salaId;
        }
    }
}
