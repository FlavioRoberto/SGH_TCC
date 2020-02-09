using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Implementacao.Cargos.Comandos.Criar;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CargoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarCargoComando comando)
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
