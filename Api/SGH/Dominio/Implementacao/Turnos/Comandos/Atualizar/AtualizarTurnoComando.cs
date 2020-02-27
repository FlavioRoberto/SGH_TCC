using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComando : IRequest<Resposta<Turno>>
    {
        public int TurnoId { get; set; }
        public string Descricao { get; set; }
    }
}
