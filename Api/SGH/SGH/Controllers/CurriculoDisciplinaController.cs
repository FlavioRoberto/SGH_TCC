using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/curriculo/disciplinas")]
    public class CurriculoDisciplinaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurriculoDisciplinaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("admin")]
        public async Task<IActionResult> Criar([FromBody] CriarCurriculoDisciplinaComando comando)
        {
            try
            {
                var resposta = await _mediator.Send(comando);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
