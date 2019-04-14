using Dominio.Model;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos;
using Servico.Implementacao;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private ITurnoServico _servico;
        public TurnoController(IRepositorio<Turno> repositorio)
        {
            _servico = new TurnoServico(repositorio);
        }

        [HttpGet]
        [Route("listarTodos")]
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
        public IActionResult ListarPorPaginacao([FromBody] Paginacao<Turno> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<Turno>();
                
                var resultado = _servico.ListarComPaginacao(entidadePaginada);

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
        public IActionResult Criar([FromBody]Turno entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                var resutltado = _servico.Criar(entidade);

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
        public IActionResult Editar([FromBody] Turno entidade)
        {
            try
            {
                var resultado = _servico.Atualizar(entidade);

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
                var resultado = _servico.Remover(codigo);

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
