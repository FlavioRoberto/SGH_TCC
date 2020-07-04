using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Base;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar
{
    public class CriarTurnoComando : TurnoComando, IRequest<Resposta<TurnoViewModel>>
    {
    }
}
