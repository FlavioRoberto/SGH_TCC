using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Curriculos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCurriculoConsulta : IRequest<Resposta<Paginacao<Curriculo>>>
    {
        public Paginacao<Curriculo> CurriculoPaginado { get; set; }
    }
}
