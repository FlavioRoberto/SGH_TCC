using Dominio.ViewModel.CurriculoViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    public class CurriculoController : ControllerBase
    {

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CurriculoViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                return Ok("testte");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
