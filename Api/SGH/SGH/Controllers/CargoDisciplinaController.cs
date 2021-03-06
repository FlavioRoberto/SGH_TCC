﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Editar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarTodas;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/cargo/disciplinas")]
    [Authorize("coordenacao")]
    public class CargoDisciplinaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CargoDisciplinaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize("admin")]
        [Route("{codigoCargo}")]
        public async Task<IActionResult> ListarDisciplinasCargo(int codigoCargo)
        {
            try
            {
                var comando = new ListarTodasDisciplinasCargoConsulta
                {
                    CodigoCargo = codigoCargo
                };

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
        public async Task<IActionResult> Criar([FromBody] CriarCargoDisciplinaComando comando)
        {
            try
            {
                var resposta = await _mediator.Send(comando);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("listar-por-curriculo")]
        public async Task<IActionResult> ListarDisciplinasCurriculo([FromBody] ListarDisciplinaCargoPorCurriculoConsulta consulta)
        {
            try
            {
                var resposta = await _mediator.Send(consulta);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Authorize("admin")]
        public async Task<IActionResult> Editar([FromBody] EditarCargoDisciplinaComando comando)
        {
            try
            {
                var resposta = await _mediator.Send(comando);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize("admin")]
        [Route("{codigo}")]
        public async Task<IActionResult> Remover(int codigo)
        {
            try
            {
                var comando = new RemoverCargoDisciplinaComando
                {
                    Codigo = codigo
                };

                var resultado = await _mediator.Send(comando);

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
