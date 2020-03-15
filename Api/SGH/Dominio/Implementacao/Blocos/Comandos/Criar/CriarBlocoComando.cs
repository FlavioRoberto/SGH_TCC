using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Blocos.Base;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar
{
    public class CriarBlocoComando : BlocoComandoBase, IRequest<Resposta<BlocoViewModel>>
    {
    }
}
