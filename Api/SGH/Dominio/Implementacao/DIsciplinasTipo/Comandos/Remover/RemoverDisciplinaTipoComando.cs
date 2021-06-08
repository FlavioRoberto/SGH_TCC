using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover
{
    public class RemoverDisciplinaTipoComando: IRequest<Resposta<bool>>
    {
        public long Codigo { get; set; }
    }
}
