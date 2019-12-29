using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacao.Contratos;
using System;
using System.Threading.Tasks;
using SGH.APi.ViewModel;

namespace SGH.Api.Controllers
{
    [Route("api/perfil")]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly IServicoBase<UsuarioPerfilViewModel> _servico;

        public UsuarioPerfilController(IUsuarioPerfilService servico)
        {
            _servico = servico;
        }

        [HttpGet]
        [Route("ListarTodos")]
        [Authorize("todos")]
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
    }
}
