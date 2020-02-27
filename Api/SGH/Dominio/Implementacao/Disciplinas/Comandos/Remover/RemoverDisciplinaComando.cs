using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Remover
{
    public class RemoverDisciplinaComando : IRequest<Resposta<bool>>
    {
        public long CodigoDisciplina { get; set; }
    }
}
