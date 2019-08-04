using AutoMapper;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.Contratos;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.DisciplinaControllers
{
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _servico;

        public DisciplinaController(IDisciplinaService servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Authorize("todos")]
        [Route("listarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var result = await _servico.ListarTodos();

                if (result.TemErro())
                    return BadRequest(result.GetErros());

                return Ok(result.GetResultado());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize("todos")]
        [Route("listarPorDescricao")]
        public async Task<IActionResult> listarPorDescricao([FromQuery] string filtro)
        {
            try
            {
                var result = await _servico.listarPorDescricao(filtro);

                if (result.TemErro())
                    return BadRequest(result.GetErros());

                return Ok(result.GetResultado());

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

                Resposta<Paginacao<DisciplinaViewModel>> resultado = await _servico.ListarComPaginacao(entidadePaginada);

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
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]DisciplinaViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<DisciplinaViewModel> resutltado = await _servico.Criar(entidade);

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
        public async Task<IActionResult> Editar([FromBody] DisciplinaViewModel entidade)
        {
            try
            {
                Resposta<DisciplinaViewModel> resultado = await _servico.Atualizar(entidade);

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
                Resposta<bool> resultado = await _servico.Remover(codigo);

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
