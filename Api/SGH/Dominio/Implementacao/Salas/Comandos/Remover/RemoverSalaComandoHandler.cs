using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Remover
{
    public class RemoverSalaComandoHandler : IRequestHandler<RemoverSalaComando, Resposta<bool>>
    {
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly IValidador<RemoverSalaComando> _validador;
    
        public RemoverSalaComandoHandler(ISalaRepositorio salaRepositorio,
                                         IValidador<RemoverSalaComando> validador)
        {
            _salaRepositorio = salaRepositorio;
            _validador = validador;
        }
       
        public async Task<Resposta<bool>> Handle(RemoverSalaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<bool>(erro);

            var resultado = await _salaRepositorio.Remover(lnq => lnq.Codigo == request.Codigo);

            return new Resposta<bool>(resultado);
            
        }
    }
}
