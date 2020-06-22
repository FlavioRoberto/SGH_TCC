using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover
{
    public class RemoverBlocoComandoValidador : AbstractValidator<RemoverBlocoComando>, IValidador<RemoverBlocoComando>
    {
        private readonly IBlocoRepositorio _blocoRepositorio;
        private readonly ISalaRepositorio _salaRepositorio;

        public RemoverBlocoComandoValidador(IBlocoRepositorio blocoRepositorio, ISalaRepositorio salaRepositorio)
        {
            _blocoRepositorio = blocoRepositorio;
            _salaRepositorio = salaRepositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O código do bloco não foi informado.");

            When(lnq => lnq.Codigo > 0, () =>
            {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeBlocoExiste).WithMessage(c => $"Não foi encontrado um bloco com o código {c.Codigo}.");
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeBlocoVinculadoASala).WithMessage(c => $"Não foi possível remover o bloco de código {c.Codigo}, pois ele está vinculado em salas.");
            });
        }

        private async Task<bool> ValidarSeBlocoVinculadoASala(int codigoBloco, CancellationToken arg2)
        {
            var resultado = await _salaRepositorio.Contem(lnq => lnq.CodigoBloco == codigoBloco);
            return !resultado;
        }

        private async Task<bool> ValidarSeBlocoExiste(int codigo, CancellationToken arg2)
        {
            return await _blocoRepositorio.Contem(lnq => lnq.Codigo == codigo);
        }
    }
}
