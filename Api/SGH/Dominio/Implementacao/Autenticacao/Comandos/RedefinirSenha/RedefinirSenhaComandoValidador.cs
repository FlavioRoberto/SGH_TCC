﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Repositorio.Contratos;
using Aplicacao.Implementacao.Autenticacao.Contratos;

namespace Aplicacao.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComandoValidador : AbstractValidator<RedefinirSenhaComando>, IRedefinirSenhaComandoValidador
    {
        private readonly IUsuarioRepositorio _repositorio;

        public RedefinirSenhaComandoValidador(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.Email).NotEmpty().WithMessage("O campo de e-mail é obrigatório.");
            RuleFor(lnq => lnq.Email).MustAsync(ValidarEmailExistente).WithMessage(comando => $"Não foi encontrado um usuário vinculado com o e-mail {comando.Email}!");
        }

        private async Task<bool> ValidarEmailExistente(string email, CancellationToken cancellationToken)
        {
            var usuario = await _repositorio.Listar(lnq => lnq.Email.Equals(email));
            if (usuario == null)
                return false;
            return true;
        }
    }
}