﻿using Global;
using Repositorio.Contratos;
using Aplicacao.Extensions;
using Aplicacao.Implementacao.Autenticacao.Contratos;
using System.Threading.Tasks;
using MediatR;
using System.Threading;

namespace Aplicacao.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComandoHandler : IRequestHandler<LoginComando, Resposta<string>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly ILoginComandoValidator _validador;

        public LoginComandoHandler(IUsuarioRepositorio repositorio, ILoginComandoValidator validator)
        {
            _repositorio = repositorio;
            _validador = validator;
        }

        public async Task<Resposta<string>> Handle(LoginComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>("", erros);

            var usuario = await _repositorio.RetornarUsuarioPorLoginESenha(request.Login, request.Senha);

            string token = TokenGeradorHelper.Gerar(usuario);

            return new Resposta<string>(token);
        }
    }
}