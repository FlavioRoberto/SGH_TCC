using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Remover
{
    public class RemoverCargoComandoHandler : IRequestHandler<RemoverCargoComando, Resposta<bool>>
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IValidador<RemoverCargoComando> _validador;

        public RemoverCargoComandoHandler(ICargoRepositorio cargoRepositorio, ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, IValidador<RemoverCargoComando> validador)
        {
            _cargoRepositorio = cargoRepositorio;
            _validador = validador;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
        }

        public async Task<Resposta<bool>> Handle(RemoverCargoComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<bool>(erro);

            var resultado = false;

            await _cargoDisciplinaRepositorio.IniciarTransacao();

            var disciplinas = await _cargoDisciplinaRepositorio.Listar(lnq => lnq.CodigoCargo == request.Codigo);

            foreach (var disciplina in disciplinas)
                await _cargoDisciplinaRepositorio.Remover(lnq => lnq.Codigo == disciplina.Codigo);

            resultado = _cargoRepositorio.Remover(lnq => lnq.Codigo == request.Codigo).Result;

            _cargoDisciplinaRepositorio.FecharTransacao();

            return new Resposta<bool>(resultado);
        }
    }
}
