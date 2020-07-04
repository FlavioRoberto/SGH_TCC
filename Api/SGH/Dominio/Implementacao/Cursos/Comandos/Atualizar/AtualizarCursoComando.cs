using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Base;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Atualizar
{
    public class AtualizarCursoComando : CursoComando, IRequest<Resposta<Curso>>
    {
        public int? Codigo { get; set; }
    }
}
