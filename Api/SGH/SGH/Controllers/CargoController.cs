using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Cargos.Consultas.ListarPaginacao;
using SGH.Dominio.ViewModel;
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

        [HttpPut]
        [Authorize("admin")]
        [Route("editar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarCargoComando comando)
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
        [Route("remover")]
        public async Task<IActionResult> Remover([FromQuery] int codigo)
        {
            try
            {
                var comando = new RemoverCargoComando
                {
                    Codigo = codigo
                };

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
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<CargoViewModel> entidadePaginada)
        {
            try
            {
                var consulta = new ListarPaginacaoCargoConsulta
                {
                    CargoPaginado = entidadePaginada
                };

                var resultado = await _mediator.Send(consulta);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
