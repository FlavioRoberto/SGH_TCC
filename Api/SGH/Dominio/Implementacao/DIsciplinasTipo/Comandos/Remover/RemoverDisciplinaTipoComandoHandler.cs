using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover
{
    public class RemoverDisciplinaTipoComandoHandler : IRequestHandler<RemoverDisciplinaTipoComando, Resposta<bool>>
    {
        private readonly IDisciplinaTipoRepositorio _repositorio;
        private readonly IValidador<RemoverDisciplinaTipoComando> _validador;


        public RemoverDisciplinaTipoComandoHandler(IDisciplinaTipoRepositorio repositorio, IValidador<RemoverDisciplinaTipoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverDisciplinaTipoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);
            
            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.Codigo);
            return new Resposta<bool>(resultado);
        }
    }
}
