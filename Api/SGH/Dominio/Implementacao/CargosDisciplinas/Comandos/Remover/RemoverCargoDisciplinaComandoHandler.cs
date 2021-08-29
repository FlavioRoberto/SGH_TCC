using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Remover
{
    public class RemoverCargoDisciplinaComandoHandler : IRequestHandler<RemoverCargoDisciplinaComando, Resposta<bool>>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IValidador<RemoverCargoDisciplinaComando> _validador;

        public RemoverCargoDisciplinaComandoHandler(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, IValidador<RemoverCargoDisciplinaComando> validador)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverCargoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<bool>(erro);

            var resultado = await _cargoDisciplinaRepositorio.Remover(lnq => lnq.Codigo == request.Codigo);

            return new Resposta<bool>(resultado);
        }
    }
}
