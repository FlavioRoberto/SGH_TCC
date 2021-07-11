using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.DefinirSala;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Lancar;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Remover;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/aula")]
    public class AulaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("criar")]
        [Authorize("coordenacao")]
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

        [HttpPost]
        [Route("lancar")]
        [Authorize("coordenacao")]
        public async Task<IActionResult> Lancar([FromBody] LancarAulasComando lancarAulaComando)
        {
            try
            {
                var resultado = await _mediator.Send(lancarAulaComando);

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
        [Route("definir-sala")]
        [Authorize(Roles = "infraestrutura")]
        public async Task<IActionResult> DefinirSala([FromBody] DefinirSalaComando definirSalaComando)
        {
            try
            {
                var resultado = await _mediator.Send(definirSalaComando);

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
        [Authorize("coordenacao")]
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
