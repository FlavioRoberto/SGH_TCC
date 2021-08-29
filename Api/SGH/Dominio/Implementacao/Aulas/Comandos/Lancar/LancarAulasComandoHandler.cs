using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Lancar
{
    public class LancarAulasComandoHandler : IRequestHandler<LancarAulasComando, Resposta<List<string>>>
    {
        private readonly IMediator _mediator;
        private readonly IValidador<LancarAulasComando> _validator;

        public LancarAulasComandoHandler(IMediator mediator,
                                         IValidador<LancarAulasComando> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task<Resposta<List<string>>> Handle(LancarAulasComando request, CancellationToken cancellationToken)
        {
            var erro = _validator.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<List<string>>(erro);

            var erros = new List<string>();

            foreach (var reserva in request.Reservas)
            {
                var cadastroAula = await _mediator.Send(new CriarAulaComando
                {
                    CodigoDisciplina = request.CodigoDisciplina,
                    CodigoHorario = request.CodigoHorario,
                    CodigoSala = request.CodigoSala,
                    Laboratorio = request.Laboratorio,
                    Reserva = reserva
                });

                if (cadastroAula.TemErro())
                    erros.Add(cadastroAula.GetErros());
            }

            return new Resposta<List<string>>(erros);

        }
    }
}
