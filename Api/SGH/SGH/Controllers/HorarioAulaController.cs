using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Horarios.Consultas.Listar;
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

        [HttpPost]
        [Authorize("admin")]
        [Route("listar")]
        public async Task<IActionResult> Listar([FromBody] ListarHorarioAulaConsulta consulta)
        {
            try
            {
                var resultado = await _mediator.Send(consulta);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize("admin")]
        [Route("remover")]
        public async Task<IActionResult> Remover([FromQuery]int codigo)
        {
            try
            {
                Resposta<Unit> resultado = await _mediator.Send(new RemoverHorarioComando
                {
                    CodigoHorario = codigo
                });

                if (resultado.TemErro())
                    return BadRequest(resultado.GetErros());

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
