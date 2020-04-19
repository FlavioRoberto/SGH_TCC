using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Comum;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar
{
    public class CriarHorarioAulaComando: HorarioAulaComando, IRequest<Resposta<HorarioAulaViewModel>>
    {
    }
}
