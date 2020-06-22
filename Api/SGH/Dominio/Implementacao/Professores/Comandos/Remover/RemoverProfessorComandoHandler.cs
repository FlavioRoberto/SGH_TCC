using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover
{
    public class RemoverProfessorComandoHandler : IRequestHandler<RemoverProfessorComando, Resposta<bool>>
    {
        private IProfessorRepositorio _repositorio;
        private IValidador<RemoverProfessorComando> _validador;

        public RemoverProfessorComandoHandler(IProfessorRepositorio repositorio, IValidador<RemoverProfessorComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverProfessorComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.ProfessorId);
            return new Resposta<bool>(resultado);
        }
    }
}