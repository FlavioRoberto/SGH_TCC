using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Atualizar
{
    public class AtualizarDisciplinaComando : DisciplinaComandoBase, IRequest<Resposta<Disciplina>>
    {
    }
}
