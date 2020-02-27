using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.UsuarioPerfis.Comando.Remover
{
    public class RemoverUsuarioPerfilHandler : IRequestHandler<RemoverUsuarioPerfilComando, Resposta<bool>>
    {
        private readonly IUsuarioPerfilRepositorio _repositorio;
        private readonly IRemoverUsuarioPerfilComandoValidador _validador;

        public RemoverUsuarioPerfilHandler(IUsuarioPerfilRepositorio repositorio, IRemoverUsuarioPerfilComandoValidador validador)
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
