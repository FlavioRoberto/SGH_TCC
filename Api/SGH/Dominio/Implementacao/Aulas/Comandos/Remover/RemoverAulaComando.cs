using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Remover
{
    public class RemoverAulaComando: IRequest<Resposta<Unit>>
    {
        public int CodigoAula { get; set; }
    }
}
