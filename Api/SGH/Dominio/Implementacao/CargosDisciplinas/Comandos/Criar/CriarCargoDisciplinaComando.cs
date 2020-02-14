using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Criar
{
    public class CriarCargoDisciplinaComando : IRequest<Resposta<CargoDisciplinaViewModel>>
    {
    }
}
