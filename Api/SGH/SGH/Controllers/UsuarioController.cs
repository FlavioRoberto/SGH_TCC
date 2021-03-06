﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Usuarios.Consultas.ListarPaginacao;
using AutoMapper;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.Login;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.AtualizarSenha;

namespace Api.Controllers.Autenticacao
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsuarioController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("autenticar")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginComando comando)
        {
            try
            {               
                Resposta<string> resposta = await _mediator.Send(comando);

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
                var resposta = await _mediator.Send(new RedefinirSenhaComando
                {
                    Email = email
                });

                if (resposta.TemErro())
                    return BadRequest(resposta.GetErros());

                return Ok(resposta.GetResultado());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("atualizarSenha")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarSenha([FromBody] AtualizacaoSenhaViewModel viewModel)
        {
            try
            {
                var resposta = await _mediator.Send(new AtualizarSenhaComando
                {
                    Senha = viewModel.Senha,
                    NovaSenha = viewModel.NovaSenha
                });

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

                var usuarioPaginado = _mapper.Map<Paginacao<Usuario>>(entidadePaginada);

                var resultado = await _mediator.Send(new ListarPaginacaoUsuarioConsulta
                {
                    UsuarioPaginado = usuarioPaginado
                });

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize("admin")]
        [Route("criar")]
        public async Task<IActionResult> Criar([FromBody]CriarUsuarioComando comando)
        {
            try
            {
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

        [HttpPut]
        [Route("editar")]
        [Authorize("admin")]
        public async Task<IActionResult> Editar([FromBody] AtualizarUsuarioComando comando)
        {
            try
            {
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

        [HttpDelete]
        [Route("remover")]
        [Authorize("admin")]
        public async Task<IActionResult> Remover([FromQuery]int codigo)
        {
            try
            {
                var resultado = await _mediator.Send(new RemoverUsuarioComando
                {
                    CodigoUsuario = codigo
                });

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
