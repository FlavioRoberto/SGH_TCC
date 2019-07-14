using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos;
using Servico.Implementacao.DisciplinaImp;
using System;

namespace Api.Controllers.DisciplinaControllers
{
    [Route("api/[controller]")]
    public class DisciplinaTipoController : ControllerBase
    {
        private readonly IServicoBase<DisciplinaTipoViewModel> _servico;

        public DisciplinaTipoController(IRepositorio<DisciplinaTipo> repositorio, IMapper mapper)
        {
            _servico = new DisciplinaTipoServico(repositorio, mapper);
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
        public IActionResult ListarPorPaginacao([FromBody] Paginacao<DisciplinaTipoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<DisciplinaTipoViewModel>();

                Resposta<Paginacao<DisciplinaTipoViewModel>> resultado = _servico.ListarComPaginacao(entidadePaginada);

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
        public IActionResult Criar([FromBody]DisciplinaTipoViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<DisciplinaTipoViewModel> resutltado = _servico.Criar(entidade);

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
        public IActionResult Editar([FromBody] DisciplinaTipoViewModel entidade)
        {
            try
            {
                Resposta<DisciplinaTipoViewModel> resultado = _servico.Atualizar(entidade);

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
