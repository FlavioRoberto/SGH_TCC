using Dominio.ViewModel;
using Dominio.ViewModel.AutenticacaoViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.Contratos;
using System;
using System.Threading.Tasks;
using Global;
using Servico.Implementacao.Autenticacao.Comandos.Login;
using Servico.Implementacao.Autenticacao.Contratos;

namespace Api.Controllers.Autenticacao
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _servico;
        private readonly IAutenticacaoService _autenticacaoService;

        public UsuarioController(IUsuarioService servico, IAutenticacaoService autenticacaoService)
        {
            _servico = servico;
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("autenticar")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            try
            {
                var loginComando = new LoginComando
                {
                    Login = login.Login,
                    Senha = login.Senha
                };

                Resposta<string> resposta = await _autenticacaoService.Autenticar(loginComando);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("redefinirSenha")]
        [AllowAnonymous]
        public async Task<IActionResult> RedefinirSenha([FromBody] string email)
        {
            try
            {
                var resposta = await _servico.RedefinirSenha(email);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("atualizarSenha")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarSenha([FromBody] AtualizacaoSenhaViewModel viewModel)
        {
            try
            {
                var resposta = await _servico.AtualizarSenha(viewModel.Senha, viewModel.NovaSenha);

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Authorize("todos")]
        [Route("listarPaginacao")]
        public async Task<IActionResult> ListarPorPaginacao([FromBody] Paginacao<UsuarioViewModel> entidadePaginada)
        {
            try
            {
                if (entidadePaginada == null)
                    entidadePaginada = new Paginacao<UsuarioViewModel>();

                Resposta<Paginacao<UsuarioViewModel>> resultado = await _servico.ListarComPaginacao(entidadePaginada);

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
        public async Task<IActionResult> Criar([FromBody]UsuarioViewModel entidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Dados informados inválidos!");

                Resposta<UsuarioViewModel> resutltado = await _servico.Criar(entidade);

                if (resutltado.TemErro())
                    return BadRequest(resutltado.GetErros());

                return Ok(resutltado.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("editar")]
        [Authorize("admin")]
        public async Task<IActionResult> Editar([FromBody] UsuarioViewModel entidade)
        {
            try
            {
                Resposta<UsuarioViewModel> resultado = await _servico.Atualizar(entidade);

                if (resultado.TemErro())
                    return BadRequest(resultado.GetErros());

                return Ok(resultado.GetResultado());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("remover")]
        [Authorize("admin")]
        public async Task<IActionResult> Remover([FromQuery]int codigo)
        {
            try
            {
                Resposta<bool> resultado = await _servico.Remover(codigo);

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
