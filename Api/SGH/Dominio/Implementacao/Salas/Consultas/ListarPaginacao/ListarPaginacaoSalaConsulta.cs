using MediatR;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Salas.Consultas.ListarPaginacao
{
    public class ListarPaginacaoSalaConsulta : IRequest<Paginacao<Sala>>
    {
        public Paginacao<SalaViewModel> SalaPaginado { get; set; }
    }
}
