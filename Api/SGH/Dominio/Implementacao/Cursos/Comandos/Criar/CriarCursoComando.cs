using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Base;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Criar
{
    public class CriarCursoComando : CursoComando, IRequest<Resposta<Curso>>
    {
    }
}
