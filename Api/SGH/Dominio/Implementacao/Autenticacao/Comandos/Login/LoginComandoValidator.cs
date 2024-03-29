﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComandoValidator : Validador<LoginComando>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private Usuario _usuario;

        public LoginComandoValidator(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(rule => rule.Login).NotEmpty().WithMessage("O campo de login não pode ser vazio.");
            RuleFor(rule => rule.Senha).NotEmpty().WithMessage("O campo de senha não pode ser vazio.");

            When(lnq => !string.IsNullOrEmpty(lnq.Login) && !string.IsNullOrEmpty(lnq.Senha), () =>
            {
                RuleFor(rule => rule).MustAsync(ValidarAutenticacao).WithMessage("Usuário e/ou senha inválidos!");
            });

            When(lnq => _usuario != null, () =>
            {
                RuleFor(rule => rule).Must(ValidarUsuarioAtivo).WithMessage("Não foi possível logar no sistema, o usuário informado está inativo!");
            });
        }


        private async Task<bool> ValidarAutenticacao(LoginComando loginComando, CancellationToken cancellationToken)
        {
            _usuario = await _repositorio.RetornarUsuarioPorLoginESenha(loginComando.Login, loginComando.Senha);

            if (_usuario == null)
                return false;

            return true;
        }

        private bool ValidarUsuarioAtivo(LoginComando arg)
        {
            if (_usuario.Ativo)
                return true;

            return false;
        }

    }
}
