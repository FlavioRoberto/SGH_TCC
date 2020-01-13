using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Cursos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCursoConsulta : IRequest<Resposta<Paginacao<Curso>>>
    {
        public Paginacao<Curso> CursoPaginado { get; set; }
    }
}
