using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Remover
{
    public class RemoverDisciplinaComandoHandler : IRequestHandler<RemoverDisciplinaComando, Resposta<bool>>
    {
        private readonly IDisciplinaRepositorio _repositorio;
        private readonly IValidador<RemoverDisciplinaComando> _validador;

        public RemoverDisciplinaComandoHandler(IDisciplinaRepositorio repositorio, IValidador<RemoverDisciplinaComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverDisciplinaComando request, CancellationToken cancellationToken)
        {
            string erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var result = await _repositorio.Remover(lnq => lnq.Codigo == request.CodigoDisciplina);
            return new Resposta<bool>(result);
        }
    }
}
