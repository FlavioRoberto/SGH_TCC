using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar
{
    public class CriarCurriculoDisciplinaComando : CurriculoDisciplinaComandoBase, IRequest<Resposta<CurriculoDisciplinaViewModel>>
    {
    }
}
