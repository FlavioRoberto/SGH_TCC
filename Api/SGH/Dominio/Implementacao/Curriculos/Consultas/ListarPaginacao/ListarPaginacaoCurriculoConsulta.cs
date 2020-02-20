using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Implementacao.Curriculos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCurriculoConsulta : IRequest<Paginacao<CurriculoViewModel>>
    {
        public Paginacao<Curriculo> CurriculoPaginado { get; set; }
    }
}
