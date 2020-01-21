using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Consultas.ListarPaginacao
{
    public class ListarPaginacaoDisciplinaTipoConsulta : IRequest<Paginacao<DisciplinaTipo>>
    {
        public Paginacao<DisciplinaTipo> DisciplinaTipoPaginacao { get; set; }
    }
}
