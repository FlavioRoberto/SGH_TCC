using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Remover
{
    public class RemoverDisciplinaTipoComandoHandler : IRequestHandler<RemoverDisciplinaTipoComando, Resposta<bool>>
    {
        private readonly IDisciplinaRepositorio _repositorio;
        private readonly IRemoverDisciplinaTipoComandoValidador _validador;


        public RemoverDisciplinaTipoComandoHandler(IDisciplinaRepositorio repositorio, IRemoverDisciplinaTipoComandoValidador validador)
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
