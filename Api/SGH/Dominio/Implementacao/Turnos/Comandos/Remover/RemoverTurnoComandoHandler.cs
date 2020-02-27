using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover
{
    public class RemoverTurnoComandoHandler : IRequestHandler<RemoverTurnoComando, Resposta<bool>>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly IRemoverTurnoComandoValidador _validador;

        public RemoverTurnoComandoHandler(ITurnoRepositorio repositorio, IRemoverTurnoComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverTurnoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.TurnoId);

            return new Resposta<bool>(resultado);
        }
    }
}
