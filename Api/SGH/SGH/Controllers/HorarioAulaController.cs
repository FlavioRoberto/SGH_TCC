using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/horario-aula")]
    public class HorarioAulaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HorarioAulaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarHorarioAulaComando comando)
        {
            try
            {
                var resultado = await _mediator.Send(comando);

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
