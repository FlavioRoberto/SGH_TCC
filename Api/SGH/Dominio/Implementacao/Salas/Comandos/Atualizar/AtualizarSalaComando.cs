using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Base;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar
{
    public class AtualizarSalaComando : SalaComando, IRequest<Resposta<SalaViewModel>>
    {
        public int Codigo { get; set; }
    }
}
