﻿using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComandoHandler : IRequestHandler<AtualizarUsuarioComando, Resposta<Usuario>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IValidador<AtualizarUsuarioComando> _validador;

        public AtualizarUsuarioComandoHandler(IUsuarioRepositorio repositorio, IValidador<AtualizarUsuarioComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Usuario>> Handle(AtualizarUsuarioComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Usuario>(erros);

            var usuario = new Usuario
            {
                Codigo = request.Codigo ?? 0,
                Ativo = request.Ativo,
                Email = request.Email,
                Foto = request.Foto,
                Login = request.Login,
                Nome = request.Nome,
                PerfilCodigo = request.PerfilCodigo,
                Senha = request.Senha,
                Telefone = request.Telefone,
                CursoCodigo = request.CursoCodigo
            };

            var resultado = await _repositorio.Atualizar(usuario);
            return new Resposta<Usuario>(resultado);
        }
    }
}
