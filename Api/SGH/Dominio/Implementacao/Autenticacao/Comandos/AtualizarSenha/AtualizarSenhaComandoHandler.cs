﻿using Aplicacao.Contratos;
using System.Threading.Tasks;
using Global.Extensions;
using Global;
using Repositorio.Contratos;
using Aplicacao.Implementacao.Autenticacao.Contratos;
using Aplicacao.Extensions;
using MediatR;
using System.Threading;

namespace Aplicacao.Implementacao.Autenticacao.Comandos.AtualizarSenha
{
    public class AtualizarSenhaComandoHandler : IRequestHandler<AtualizarSenhaComando, Resposta<string>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IUsuarioResolverService _usuarioResolverService;
        private readonly IAtualizarSenhaComandoValidador _validador;

        public AtualizarSenhaComandoHandler(IUsuarioRepositorio repositorio, IUsuarioResolverService usuarioResolverService, IAtualizarSenhaComandoValidador validador)
        {
            _repositorio = repositorio;
            _usuarioResolverService = usuarioResolverService;
            _validador = validador;
        }


        public async Task<Resposta<string>> Handle(AtualizarSenhaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>("", erros);

            var codigoUsuarioLogado = _usuarioResolverService.GetUser().ToInt();

            var usuario = await _repositorio.Listar(lnq => lnq.Codigo == codigoUsuarioLogado);

            usuario.Senha = request.NovaSenha.ToMD5();

            await _repositorio.Atualizar(usuario);

            return new Resposta<string>("A senha foi atualizada!");
        }
    }
}