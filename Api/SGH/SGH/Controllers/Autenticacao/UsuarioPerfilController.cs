using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos;
using Servico.Implementacao.Autenticacao;
using System;
using System.Threading.Tasks;

namespace Api.Controllers.Autenticacao
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
