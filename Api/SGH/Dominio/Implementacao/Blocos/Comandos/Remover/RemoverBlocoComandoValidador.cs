using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover
{
    public class RemoverBlocoComandoValidador : AbstractValidator<RemoverBlocoComando>, IValidador<RemoverBlocoComando>
    {
        private readonly IBlocoRepositorio _blocoRepositorio;

        public RemoverBlocoComandoValidador(IBlocoRepositorio blocoRepositorio)
        {
            _blocoRepositorio = blocoRepositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O código do bloco não foi informado.");

            When(lnq => lnq.Codigo > 0, () =>
            {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeBlocoExiste).WithMessage(c => $"Não foi encontrado um bloco com o código {c.Codigo}.");
            });
        }

        private async Task<bool> ValidarSeBlocoExiste(int codigo, CancellationToken arg2)
        {
            return await _blocoRepositorio.Contem(lnq => lnq.Codigo == codigo);
        }
    }
}
