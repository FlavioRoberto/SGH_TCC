using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Remover
{
    public class RemoverCargoComando : IRequest<Resposta<bool>> 
    {
        public int Codigo { get; set; }
    }
}
