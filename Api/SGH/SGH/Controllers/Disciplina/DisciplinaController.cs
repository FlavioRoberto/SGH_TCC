using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos.DisciplinaServico;
using Servico.Implementacao.DisciplinaImp;
using System;

namespace Api.Controllers.DisciplinaControllers
{
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {

        private readonly IDisciplinaServico _servico;

        public DisciplinaController(IRepositorio<Disciplina> repositorio ,IMapper mapper)
        {
            _servico = new DisciplinaServico(repositorio, mapper);
        }

        [HttpGet]
        [Route("listarTodos")]
        public IActionResult ListarTodos()
        {
            try
            {
                var result = _servico.ListarTodos();

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
        [Route("listarPaginacao")]
        public IActionResult ListarPorPaginacao([FromBody] Paginacao<DisciplinaViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<DisciplinaViewModel>();

                Resposta<Paginacao<DisciplinaViewModel>> resultado = _servico.ListarComPaginacao(entidadePaginada);

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
        public IActionResult Criar([FromBody]DisciplinaViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<DisciplinaViewModel> resutltado = _servico.Criar(entidade);

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
        public IActionResult Editar([FromBody] DisciplinaViewModel entidade)
        {
            try
            {
                Resposta<DisciplinaViewModel> resultado = _servico.Atualizar(entidade);

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
