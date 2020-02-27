using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Atualizar
{
    public class AtualizarCursoComando : IRequest<Resposta<Curso>>
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
