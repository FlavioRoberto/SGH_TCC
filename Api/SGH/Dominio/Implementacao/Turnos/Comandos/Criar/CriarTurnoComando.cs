using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar
{
    public class CriarTurnoComando : IRequest<Resposta<TurnoViewModel>>
    {
        public string Descricao { get; set; }
        public string[] Horarios { get; set; }

    }
}
