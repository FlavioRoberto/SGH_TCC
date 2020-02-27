using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SGH.Dominio.ViewModel;
using MediatR;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Consultas.ListarTodos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Consultas.ListarPaginacao;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover;

namespace SGH.Api.Controllers
{
    [Route("api/[controller]")]
    public class DisciplinaTipoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DisciplinaTipoController(IMediator mediator, IMapper mapper)
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
                var resultado = await _mediator.Send(new ListarTodosDisciplinaTipoConsulta());
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
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<DisciplinaTipoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<DisciplinaTipoViewModel>();

                var disciplinaPaginada = _mapper.Map<Paginacao<DisciplinaTipo>>(entidadePaginada);

                var resultado = await _mediator.Send(new ListarPaginacaoDisciplinaTipoConsulta
                {
                    DisciplinaTipoPaginacao = disciplinaPaginada
                });

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhum tipo de disciplina encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CriarDisciplinaTipoComando comando)
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
        public async Task<IActionResult> Editar([FromBody] AtualizarDisciplinaTipoComando comando)
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
                var resultado = await _mediator.Send(new RemoverDisciplinaTipoComando
                {
                    Codigo = codigo
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
