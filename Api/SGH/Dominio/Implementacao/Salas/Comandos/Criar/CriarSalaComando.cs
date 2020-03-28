using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Base;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar
{
    public class CriarSalaComando : SalaComando, IRequest<Resposta<SalaViewModel>>
    {      
    }
}
