using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Comum;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Atualizar
{
    public class AtualizarHorarioAulaComando : HorarioAulaComando, IRequest<Resposta<HorarioAulaViewModel>>
    {
        public int Codigo { get; set; }
      
    }
}
