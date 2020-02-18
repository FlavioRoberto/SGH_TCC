using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Remover
{
    public class RemoverCargoDisciplinaComandoHandler : IRequestHandler<RemoverCargoDisciplinaComando, Resposta<bool>>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IRemoverCargoDisciplinaComandoValidador _validador;

        public RemoverCargoDisciplinaComandoHandler(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, IRemoverCargoDisciplinaComandoValidador validador)
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
