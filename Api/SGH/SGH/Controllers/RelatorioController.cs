using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioGeral;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/relatorios")]
    public class RelatorioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatorioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("admin")]
        [AllowAnonymous]
        [Route("horario-geral")]
        public async Task<IActionResult> GerarRelatorioHorarioGeral([FromBody] GerarHorarioGeralRelatorioConsulta consulta)
        {

            var resultado = await _mediator.Send(consulta);

            if (resultado.TemErro())
                return BadRequest(resultado.GetErros());

            return Ok(resultado.GetResultado());

        }
    }
}
