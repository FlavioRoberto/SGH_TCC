using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Remover
{
    public class RemoverUsuarioComandoHandler : IRequestHandler<RemoverUsuarioComando, Resposta<bool>>
    {
       
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IRemoverUsuarioComandoValidador _validador;

        public RemoverUsuarioComandoHandler(IUsuarioRepositorio repositorio, IRemoverUsuarioComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverUsuarioComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.CodigoUsuario);

            return new Resposta<bool>(resultado);

        }
    }
}
