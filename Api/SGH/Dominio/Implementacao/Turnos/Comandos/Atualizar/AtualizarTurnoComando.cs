using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Base;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComando : TurnoComando, IRequest<Resposta<TurnoViewModel>>
    {
        public int? Codigo { get; set; }
    }
}
