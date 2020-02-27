using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SGH.Dominio.ViewModel;
using MediatR;
using AutoMapper;
using SGH.Dominio.Services.Implementacao.Professores.Consultas.ListarAtivos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Professores.Consultas.ListarPaginacao;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProfessorController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize("todos")]
        [Route("ListarAtivos")]
        public async Task<IActionResult> ListarAtivos()
        {
            try
            {
                var resultado = await _mediator.Send(new ListarProfessoresAtivosConsulta());

                if (resultado.Count > 0)
                    return Ok(resultado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<ProfessorViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<ProfessorViewModel>();

                var professorPaginado = _mapper.Map<Paginacao<Professor>>(entidadePaginada);

                var resultado = await _mediator.Send(new ListarPaginacaoProfessorConsulta
                {
                    ProfessorPaginado = professorPaginado
                });

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhum professor encontrado.");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CriarProfessorComando comando)
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
        public async Task<IActionResult> Editar([FromBody] AtualizarProfessorComando comando)
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
                var resultado = await _mediator.Send(new RemoverProfessorComando
                {
                    ProfessorId = codigo
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
