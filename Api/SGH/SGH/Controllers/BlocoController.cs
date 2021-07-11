using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Blocos.Consultas.ListarPaginacao;
using SGH.Dominio.Services.Implementacao.Blocos.Consultas.ListarTodos;
using SGH.Dominio.Services.ViewModel;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("infraestrutura")]
    public class BlocoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlocoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("listarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            var resultado = await _mediator.Send(new ListarTodosBlocoConsulta());
            return Ok(resultado);
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarBlocoComando comando)
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
        [Route("editar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarBlocoComando comando)
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
        [Route("remover")]
        public async Task<IActionResult> Remover([FromQuery] int codigo)
        {
            try
            {
                var comando = new RemoverBlocoComando
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
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<BlocoViewModel> entidadePaginada)
        {
            try
            {
                var consulta = new ListarPaginacaoBlocoConsulta
                {
                    BlocoPaginado = entidadePaginada
                };

                var resultado = await _mediator.Send(consulta);

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhum bloco encontrado.");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
