using MediatR;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Professores.Consultas.ListarPaginacao
{
    public class ListarPaginacaoProfessorConsulta : IRequest<Paginacao<Professor>>
    {
        public Paginacao<Professor> ProfessorPaginado { get; set; }
    }
}
