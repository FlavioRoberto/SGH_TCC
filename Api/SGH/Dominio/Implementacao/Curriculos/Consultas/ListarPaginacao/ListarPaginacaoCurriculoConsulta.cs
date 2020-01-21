using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Curriculos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCurriculoConsulta : IRequest<Paginacao<Curriculo>>
    {
        public Paginacao<Curriculo> CurriculoPaginado { get; set; }
    }
}
