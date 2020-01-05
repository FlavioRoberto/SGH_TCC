using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Remover
{
    public class RemoverDisciplinaTipoComando: IRequest<Resposta<bool>>
    {
        public int Codigo { get; set; }
    }
}
