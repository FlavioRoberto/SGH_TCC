using MediatR;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Cargos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCargoConsulta : IRequest<Paginacao<CargoViewModel>>
    {
        public Paginacao<CargoViewModel> CargoPaginado { get; set; }
    }
}
