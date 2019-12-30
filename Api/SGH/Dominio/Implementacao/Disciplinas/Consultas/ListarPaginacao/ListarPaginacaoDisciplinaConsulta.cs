using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarPaginacao
{
    public class ListarPaginacaoDisciplinaConsulta : IRequest<Resposta<Paginacao<Disciplina>>>
    {
        public Paginacao<Disciplina> DisciplinaPaginacao { get; set; }
    }
}
