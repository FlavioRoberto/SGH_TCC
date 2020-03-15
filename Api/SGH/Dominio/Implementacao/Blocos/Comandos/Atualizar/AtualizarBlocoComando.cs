using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Blocos.Base;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Atualizar
{
    public class AtualizarBlocoComando : BlocoComandoBase, IRequest<Resposta<BlocoViewModel>>
    {
        public int Codigo { get; set; }
    }
}
