﻿using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Remover
{
    public class RemoverSalaComandoValidador : AbstractValidator<RemoverSalaComando>, IValidador<RemoverSalaComando>
    {
        private readonly ISalaRepositorio _salaRepositorio;

        public RemoverSalaComandoValidador(ISalaRepositorio salaRepositorio)
        {
            _salaRepositorio = salaRepositorio;

            RuleFor(lnq => lnq.Codigo).NotNull().GreaterThan(0).WithMessage("O campo código é obrigatório.");

            When(lnq => lnq.Codigo > 0, () =>
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeSalaExiste).WithMessage(c => $"Não foi encontrado uma sala com o código {c.Codigo}.")
            );
        }

        private async Task<bool> ValidarSeSalaExiste(int codigoSala, CancellationToken arg2)
        {
            return await _salaRepositorio.Contem(lnq => lnq.Codigo == codigoSala);
        }
    }
}
