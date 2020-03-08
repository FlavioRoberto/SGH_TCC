using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComando : CurriculoDisciplinaComandoBase, IRequest<Resposta<CurriculoDisciplinaViewModel>>
    {
        public int Codigo { get; set; }
    }
}
