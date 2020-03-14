using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar
{
    public class CriarBlocoComando : IRequest<Resposta<BlocoViewModel>>
    {
        public string Descricao { get; set; }
    }
}
