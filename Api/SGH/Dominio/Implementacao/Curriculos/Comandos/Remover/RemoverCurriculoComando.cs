using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Remover
{
    public class RemoverCurriculoComando : IRequest<Resposta<bool>>
    {
        public long CodigoCurriculo { get; set; }
    }
}
