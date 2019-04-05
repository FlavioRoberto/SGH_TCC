using Dominio.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {

        [HttpGet]
        [Route("listarPor/{id:int}")]
        public IActionResult ListarPor(int id)
        {
            try
            {
                return Ok(new List<Turno> {
                new Turno{
                   Codigo = 1,
                   Descricao = "Engenharia da computação"
                },
                new Turno{
                    Codigo  = 2,
                    Descricao = "Engenharia civil"
                }
            });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
