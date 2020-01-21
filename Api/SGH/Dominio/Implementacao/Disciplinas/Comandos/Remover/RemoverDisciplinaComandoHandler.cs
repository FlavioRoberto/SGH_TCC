using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;

namespace SGH.Dominio.Implementacao.Disciplinas.Comandos.Remover
{
    public class RemoverDisciplinaComandoHandler : IRequestHandler<RemoverDisciplinaComando, Resposta<bool>>
    {
        private readonly IDisciplinaRepositorio _repositorio;
        private readonly IRemoverDisciplinaComandoValidador _validador;

        public RemoverDisciplinaComandoHandler(IDisciplinaRepositorio repositorio, IRemoverDisciplinaComandoValidador validador)
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
