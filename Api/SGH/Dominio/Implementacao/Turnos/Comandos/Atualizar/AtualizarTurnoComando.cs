using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComando : IRequest<Resposta<TurnoViewModel>>
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string[] Horarios { get; set; }
    }
}
