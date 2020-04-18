using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Remover
{
    public class RemoverHorarioComando : IRequest<Resposta<Unit>>
    {
        public int CodigoHorario { get; set; }
    }
}
