using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.UsuarioPerfis.Comando.Remover
{
    public class RemoverUsuarioPerfilHandler : IRequestHandler<RemoverUsuarioPerfilComando, Resposta<bool>>
    {
        private readonly IUsuarioPerfilRepositorio _repositorio;
        private readonly IValidador<RemoverUsuarioPerfilComando> _validador;

        public RemoverUsuarioPerfilHandler(IUsuarioPerfilRepositorio repositorio, IValidador<RemoverUsuarioPerfilComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverUsuarioPerfilComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.Codigo);

            return new Resposta<bool>(resultado);
        }
    }
}
