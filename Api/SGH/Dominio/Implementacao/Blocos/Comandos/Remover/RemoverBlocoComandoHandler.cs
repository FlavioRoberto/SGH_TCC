using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover
{
    public class RemoverBlocoComandoHandler : IRequestHandler<RemoverBlocoComando, Resposta<bool>>
    {
        private readonly IBlocoRepositorio _blocoRepositorio;
        private readonly IValidador<RemoverBlocoComando> _validador;

        public RemoverBlocoComandoHandler(IBlocoRepositorio blocoRepositorio, IValidador<RemoverBlocoComando> validador)
        {
            _blocoRepositorio = blocoRepositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverBlocoComando request, CancellationToken cancellationToken)
        {
            var mensagemErro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(mensagemErro))
                return new Resposta<bool>(mensagemErro);

            bool resultado = await _blocoRepositorio.Remover(lnq => lnq.Codigo == request.Codigo);

            return new Resposta<bool>(resultado);
        }
    }
}
