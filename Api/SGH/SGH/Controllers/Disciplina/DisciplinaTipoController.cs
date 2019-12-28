﻿using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Aplicacao.Contratos;
using Aplicacao.Implementacao.DisciplinaImp;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        [Authorize("todos")]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<DisciplinaTipoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<DisciplinaTipoViewModel>();

                Resposta<Paginacao<DisciplinaTipoViewModel>> resultado = await _servico.ListarComPaginacao(entidadePaginada);

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
        public async Task<IActionResult> Criar([FromBody]DisciplinaTipoViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<DisciplinaTipoViewModel> resutltado = await _servico.Criar(entidade);

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
        public async Task<IActionResult> Editar([FromBody] DisciplinaTipoViewModel entidade)
        {
            try
            {
                Resposta<DisciplinaTipoViewModel> resultado = await _servico.Atualizar(entidade);

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
