using MediatR;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Blocos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoBlocoConsulta : IRequest<Paginacao<BlocoViewModel>>
    {
        public Paginacao<BlocoViewModel> BlocoPaginado { get; set; }
    }
}
