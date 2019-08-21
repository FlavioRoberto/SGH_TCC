using Dominio.ViewModel.CurriculoViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.Contratos;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    public class CurriculoController : ControllerBase
    {
        private ICurriculoService _servico;

        public CurriculoController(ICurriculoService servico)
        {
            this._servico = servico;
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CurriculoViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                var resultado = await _servico.Criar(entidade);

                if (resultado.TemErro())
                    return BadRequest(resultado.GetErros());

                return Ok(resultado.GetResultado());

                return Ok("testte");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
