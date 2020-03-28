using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Remover;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarSalaComando comando)
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

        [HttpPut]
        [Authorize("admin")]
        [Route("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarSalaComando comando)
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

        [HttpDelete]
        [Authorize("admin")]
        [Route("Remover")]
        public async Task<IActionResult> Remover([FromQuery] int codigo)
        {
            try
            {
                var resultado = await _mediator.Send(new RemoverSalaComando
                {
                    Codigo = codigo
                });

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
