﻿using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Dominio.ViewModel.AutenticacaoViewModel;
using Global;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos;
using Servico.Implementacao.Autenticacao;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Autenticacao
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoBase<UsuarioViewModel> _servico;

        public UsuarioController(IRepositorio<Usuario> repositorio, IMapper mapper)
        {
            _servico = new UsuarioServico(repositorio, mapper);
        }

        [HttpGet]
        [Route("ListarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var resultado = await _servico.ListarTodos();

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
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<UsuarioViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<UsuarioViewModel>();

                Resposta<Paginacao<UsuarioViewModel>> resultado = await _servico.ListarComPaginacao(entidadePaginada);

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
        public async Task<IActionResult> Criar([FromBody]UsuarioViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<UsuarioViewModel> resutltado = await _servico.Criar(entidade);

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
        public async Task<IActionResult> Editar([FromBody] UsuarioViewModel entidade)
        {
            try
            {
                Resposta<UsuarioViewModel> resultado = await _servico.Atualizar(entidade);

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
