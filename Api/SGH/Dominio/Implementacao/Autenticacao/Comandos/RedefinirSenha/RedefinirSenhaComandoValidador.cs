﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComandoValidador : Validador<RedefinirSenhaComando>
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
            var contem = await _repositorio.Contem(lnq => lnq.Email.Equals(email));
            return contem;
        }
    }
}
