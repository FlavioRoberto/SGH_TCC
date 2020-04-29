using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar
{
    public class CriarAulaComandoHandler : IRequestHandler<CriarAulaComando, Resposta<AulaViewModel>>
    {
        private readonly IValidador<CriarAulaComando> _validador;

        public CriarAulaComandoHandler(IValidador<CriarAulaComando> validador)
        {
            _validador = validador;
        }

        public Task<Resposta<AulaViewModel>> Handle(CriarAulaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return Task.FromResult(new Resposta<AulaViewModel>(erros));

            throw new System.NotImplementedException();
        }
    }
}
