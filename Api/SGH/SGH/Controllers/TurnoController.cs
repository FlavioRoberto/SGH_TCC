using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico;
using Servico.Contratos;
using Servico.Implementacao;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private IServicoBase<TurnoViewModel> _servico;

        public TurnoController(IRepositorio<Turno> repositorio, IMapper mapper)
        {
            _servico = new TurnoServico(repositorio, mapper);
        }

        [HttpGet]
        [Route("listarTodos")]
        public IActionResult ListarTodos()
        {
            try
            {
                Resposta<List<TurnoViewModel>> resultado = _servico.ListarTodos();

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
        public IActionResult ListarPorPaginacao([FromBody] Paginacao<TurnoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<TurnoViewModel>();

                Resposta<Paginacao<TurnoViewModel>> resultado = _servico.ListarComPaginacao(entidadePaginada);

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
        public IActionResult Criar([FromBody]TurnoViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<TurnoViewModel> resutltado = _servico.Criar(entidade);

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
        public IActionResult Editar([FromBody] TurnoViewModel entidade)
        {
            try
            {
                Resposta<TurnoViewModel> resultado = _servico.Atualizar(entidade);

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
