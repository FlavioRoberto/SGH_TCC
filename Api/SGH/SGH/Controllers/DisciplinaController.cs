using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarTodas;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarPorDescricao;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarPaginacao;
using AutoMapper;
using SGH.Dominio.Implementacao.Disciplinas.Comandos.Criar;
using SGH.Dominio.Implementacao.Disciplinas.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Disciplinas.Comandos.Remover;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DisciplinaController(IMediator mediator, IMapper mapper)
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
                var resultado = await _mediator.Send(new ListarTodasDisciplinasConsulta());

                if (resultado.TemErro())
                    return BadRequest(resultado.GetErros());

                return Ok(resultado.GetResultado());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize("todos")]
        [Route("listarPorDescricao")]
        public async Task<IActionResult> ListarPorDescricao([FromQuery] string filtro)
        {
            try
            {
                var resultado = await _mediator.Send(new ListarPorDescricaoDisciplinaConsulta { 
                    Descricao = filtro
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

        [HttpPost]
        [Authorize("todos")]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<DisciplinaViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<DisciplinaViewModel>();

                var resultado = await _mediator.Send(new ListarPaginacaoDisciplinaConsulta
                {
                    DisciplinaPaginacao = _mapper.Map<Paginacao<Disciplina>>(entidadePaginada)
                });

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhuma disciplina encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CriarDisciplinaComando comando)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

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
        public async Task<IActionResult> Editar([FromBody] AtualizarDisciplinaComando comando)
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
                Resposta<bool> resultado = await _mediator.Send(new RemoverDisciplinaComando
                {
                    CodigoDisciplina = codigo
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
