using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Remover;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/aula")]
    [Authorize("coordenacao")]
    public class AulaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarAulaComando comando)
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
        [Route("{codigoAula}")]
        public async Task<IActionResult> Remover(int codigoAula)
        {
            try
            {
                var comando = new RemoverAulaComando { CodigoAula = codigoAula };
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
