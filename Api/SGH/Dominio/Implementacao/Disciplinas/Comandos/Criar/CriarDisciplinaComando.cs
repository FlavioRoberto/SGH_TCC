using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Criar
{
    public class CriarDisciplinaComando : DisciplinaComandoBase, IRequest<Resposta<Disciplina>>
    {
    }
}
