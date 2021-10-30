using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.DefinirSala
{
    public class DefinirSalaComandoHandler : IRequestHandler<DefinirSalaComando, Resposta<bool>>
    {
        private IValidador<DefinirSalaComando> _validador;
        private IAulaRepositorio _aulaRepositorio;

        public DefinirSalaComandoHandler(IValidador<DefinirSalaComando> validador,
                                         IAulaRepositorio aulaRepositorio)
        {
            _validador = validador;
            _aulaRepositorio = aulaRepositorio;
        }

        public async Task<Resposta<bool>> Handle(DefinirSalaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var aula = await _aulaRepositorio.Consultar(request.AulaId);

            aula.CodigoSala = request.SalaId;

            var resultado = await _aulaRepositorio.Atualizar(aula);

            return new Resposta<bool>(resultado);
        }
    }
}
