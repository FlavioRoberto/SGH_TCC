using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Remover
{
    public class RemoverCurriculoDisciplinaComandoHandler : IRequestHandler<RemoverCurriculoDisciplinaComando, Resposta<bool>>
    {
        private readonly IValidador<RemoverCurriculoDisciplinaComando> _validador;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;

        public RemoverCurriculoDisciplinaComandoHandler(IValidador<RemoverCurriculoDisciplinaComando> validador, ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio)
        {
            _validador = validador;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
        }

        public async Task<Resposta<bool>> Handle(RemoverCurriculoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _curriculoDisciplinaRepositorio.Remover(request.Codigo);

            return new Resposta<bool>(resultado);

        }
    }
}
