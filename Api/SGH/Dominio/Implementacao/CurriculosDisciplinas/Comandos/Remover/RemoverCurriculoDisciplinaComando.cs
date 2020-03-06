using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Remover
{
    public class RemoverCurriculoDisciplinaComando : IRequest<Resposta<bool>>
    {
        public  int Codigo { get; set; }
    }
}
