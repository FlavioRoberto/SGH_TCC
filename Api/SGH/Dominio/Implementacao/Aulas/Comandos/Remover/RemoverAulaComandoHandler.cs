using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Remover
{
    public class RemoverAulaComandoHandler : IRequestHandler<RemoverAulaComando, Resposta<Unit>>
    {
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly IValidador<RemoverAulaComando> _validador;


        public RemoverAulaComandoHandler(IAulaRepositorio aulaRepositorio,
                                         IValidador<RemoverAulaComando> validador)
        {
            _aulaRepositorio = aulaRepositorio;
            _validador = validador;
        }

        public async Task<Resposta<Unit>> Handle(RemoverAulaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Unit>(erros);

            await _aulaRepositorio.Remover(lnq => lnq.Codigo == request.CodigoAula);

            return new Resposta<Unit>(Unit.Value);
        }
    }
}
