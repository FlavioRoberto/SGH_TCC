using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComandoHandler : IRequestHandler<EditarCurriculoDisciplinaComando, Resposta<CurriculoDisciplinaViewModel>>
    {
        public Task<Resposta<CurriculoDisciplinaViewModel>> Handle(EditarCurriculoDisciplinaComando request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
