using MediatR;

namespace SGH.Dominio.Core.Services
{
    public interface IBusService
    {
        void AdicionarNaFila(IRequest request);
    }
}
