using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Remover
{
    public class RemoverCargoDisciplinaComando : IRequest<Resposta<bool>>
    {
        public int Codigo { get; set; }
    }
}
