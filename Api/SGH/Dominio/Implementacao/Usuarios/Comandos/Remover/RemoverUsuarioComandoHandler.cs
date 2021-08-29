using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Remover
{
    public class RemoverUsuarioComandoHandler : IRequestHandler<RemoverUsuarioComando, Resposta<bool>>
    {
       
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IValidador<RemoverUsuarioComando> _validador;

        public RemoverUsuarioComandoHandler(IUsuarioRepositorio repositorio, IValidador<RemoverUsuarioComando> validador)
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
