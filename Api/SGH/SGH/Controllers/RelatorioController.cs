using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioGeral;
using SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioIndividual;
using System;
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
        [Route("horario-geral")]
        public async Task<IActionResult> GerarRelatorioHorarioGeral([FromBody] GerarHorarioGeralRelatorioConsulta consulta)
        {
            try
            {
                var resultado = await _mediator.Send(consulta);

                if (resultado.TemErro())
                    return BadRequest(resultado.GetErros());

                return Ok(resultado.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Authorize("admin")]
        [AllowAnonymous]
        [Route("horario-individual")]
        public async Task<IActionResult> GerarRelatorioHorarioIndividual([FromBody] GerarHorarioIndividualRelatorioConsulta consulta)
        {
            try
            {

                var resultado = await _mediator.Send(consulta);

                if (resultado.TemErro())
                    return BadRequest(resultado.GetErros());

                return Ok(resultado.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
