using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Salas.Consultas.ListarPaginacao;
using SGH.Dominio.Services.Implementacao.Salas.Consultas.ListarTodas;
using SGH.Dominio.Services.ViewModel;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("infraestrutura")]
    public class SalaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("listarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            var resultado = await _mediator.Send(new ListarTodasSalasConsulta());

            if (resultado.TemErro())
                return BadRequest(resultado.GetErros());

            return Ok(resultado.GetResultado());
        }

        [HttpPost]
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

        [HttpPost]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<SalaViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<SalaViewModel>();

                var resultado = await _mediator.Send(new ListarPaginacaoSalaConsulta
                {
                    SalaPaginado = entidadePaginada
                });

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhuma sala encontrada.");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize("admin")]
        [Route("editar")]
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
