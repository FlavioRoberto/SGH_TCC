using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Remover
{
    public class RemoverHorarioComandoHandler : IRequestHandler<RemoverHorarioComando, Resposta<Unit>>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;
        private readonly IValidador<RemoverHorarioComando> _validador;

        public RemoverHorarioComandoHandler(IHorarioAulaRepositorio horarioAulaRepositorio, IValidador<RemoverHorarioComando> validador)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;
            _validador = validador;
        }

        public async Task<Resposta<Unit>> Handle(RemoverHorarioComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<Unit>(erro);

            var repositorioRemover = await _horarioAulaRepositorio.Remover(lnq => lnq.Codigo == request.CodigoHorario);
            return new Resposta<Unit>(Unit.Value);
        }
    }
}
