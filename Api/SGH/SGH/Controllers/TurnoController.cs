using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPaginacao;
using SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarTodos;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TurnoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize("todos")]
        [Route("listarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var resultado = await _mediator.Send(new ListarTodosTurnoConsulta());
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize("todos")]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<TurnoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<TurnoViewModel>();

                var turnoPaginado = _mapper.Map<Paginacao<Turno>>(entidadePaginada);

                var resultado = await _mediator.Send(new ListarPaginacaoTurnoConsulta
                {
                    TurnoPaginado = turnoPaginado
                });

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhum turno encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CriarTurnoComando comando)
        {
            try
            {              
                var resutltado = await _mediator.Send(comando);

                if (resutltado.TemErro())
                    return BadRequest(resutltado.GetErros());

                return Ok(resutltado.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize("admin")]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] AtualizarTurnoComando comando)
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
        public async Task<IActionResult> Remover([FromQuery]int codigo)
        {
            try
            {
                Resposta<bool> resultado = await _mediator.Send(new RemoverTurnoComando
                {
                    TurnoId = codigo
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
