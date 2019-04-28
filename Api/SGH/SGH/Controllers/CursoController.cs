using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos;
using Servico.Implementacao;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {

        private readonly ICursoServico _servico;

        public CursoController(IRepositorio<Curso> repositorio, IMapper mapper)
        {
            _servico = new CursoServico(repositorio, mapper);
        }

        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult ListarTodos()
        {
            try
            {
                var resultado = _servico.ListarTodos();

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
        public IActionResult ListarPorPaginacao([FromBody] Paginacao<CursoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<CursoViewModel>();

                Resposta<Paginacao<CursoViewModel>> resultado = _servico.ListarComPaginacao(entidadePaginada);

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
        [Route("criar")]
        public IActionResult Criar([FromBody]CursoViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<CursoViewModel> resutltado = _servico.Criar(entidade);

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
        [Route("editar")]
        public IActionResult Editar([FromBody] CursoViewModel entidade)
        {
            try
            {
                Resposta<CursoViewModel> resultado = _servico.Atualizar(entidade);

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
        public IActionResult Remover([FromQuery]int codigo)
        {
            try
            {
                Resposta<bool> resultado = _servico.Remover(codigo);

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
