using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using Servico.Contratos;
using Servico.Implementacao.Autenticacao;
using System;

namespace Api.Controllers.Autenticacao
{
    [Route("api/[controller]")]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly IServicoBase<UsuarioPerfilViewModel> _servico;

        public UsuarioPerfilController(IRepositorio<UsuarioPerfil> repositorio, IMapper mapper)
        {
            _servico = new UsuarioPerfilServico(repositorio, mapper);
        }

        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult ListarTodos()
        {
            try
            {
                var resultado = _servico.ListarTodos();

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
