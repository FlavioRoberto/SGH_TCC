using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Criar;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/cargo/disciplinas")]
    public class CargoDisciplinaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CargoDisciplinaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("admin")]
        public async Task<IActionResult> Criar([FromBody] CriarCargoDisciplinaComando comando)
        {
            try
            {
                var resposta = await _mediator.Send(comando);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
