﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarPaginacao;
using System;
using System.Threading.Tasks;
using SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarTodos;

namespace SGH.Api.Controllers
{

    [Route("api/[controller]")]
    public class CurriculoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CurriculoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("listarTodos")]
        public async Task<IActionResult> ListarTodos(int curriculoId)
        {
            try
            {
                var resultado = await _mediator.Send(new ListarTodosCurriculosConsulta());              

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
        public async Task<IActionResult> Criar([FromBody]CriarCurriculoComando comando)
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

        [HttpPut]
        [Authorize("admin")]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] AtualizarCurriculoComando comando)
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

        [HttpPost]
        [Authorize("admin")]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<CurriculoViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<CurriculoViewModel>();

                var resultado = await _mediator.Send(new ListarPaginacaoCurriculoConsulta
                {
                    CurriculoPaginado = _mapper.Map<Paginacao<Curriculo>>(entidadePaginada)
                });

                if (resultado.Entidade != null && resultado.Entidade.Count > 0)
                    return Ok(resultado);

                return BadRequest("Nenhum currículo encontrado.");
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
                var resultado = await _mediator.Send(new RemoverCurriculoComando
                {
                    CodigoCurriculo = codigo
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
