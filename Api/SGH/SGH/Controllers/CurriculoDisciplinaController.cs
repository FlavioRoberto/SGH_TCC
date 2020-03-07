using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Consultas.ListarDisciplinas;
using System;
using System.Threading.Tasks;

namespace SGH.Api.Controllers
{
    [Route("api/curriculo/disciplinas")]
    public class CurriculoDisciplinaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurriculoDisciplinaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize("admin")]
        [Route("{curriculoId}")]
        public async Task<IActionResult> ListarDisciplinas(int curriculoId)
        {
            try
            {
                var resultado = await _mediator.Send(new ListarDisciplinasCurriculoConsulta
                {
                    CodigoCurriculo = curriculoId
                });

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
        public async Task<IActionResult> Criar([FromBody] CriarCurriculoDisciplinaComando comando)
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
        [Route("{codigoCurriculoDisciplina}")]
        public async Task<IActionResult> Remover(int codigoCurriculoDisciplina)
        {
            try
            {
                var comando = new RemoverCurriculoDisciplinaComando
                {
                    Codigo = codigoCurriculoDisciplina
                };

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
    }
}
