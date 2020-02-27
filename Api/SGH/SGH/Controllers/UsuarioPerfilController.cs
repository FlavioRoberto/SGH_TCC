using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using SGH.Dominio.Services.Implementacao.UsuarioPerfis.Consultas.ListarTodos;

namespace SGH.Api.Controllers
{
    [Route("api/perfil")]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioPerfilController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("ListarTodos")]
        [Authorize("todos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var resultado = await _mediator.Send(new ListarTodosPerfilConsulta());
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
