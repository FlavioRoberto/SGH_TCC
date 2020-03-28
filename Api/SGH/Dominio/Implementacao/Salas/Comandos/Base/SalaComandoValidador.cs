using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Base
{
    public abstract class SalaComandoValidador<T> : AbstractValidator<T> where T : SalaComando
    {
        private readonly IBlocoRepositorio _blocoRepositorio;

        public SalaComandoValidador(IBlocoRepositorio blocoRepositorio)
        {
            _blocoRepositorio = blocoRepositorio;

            RuleFor(lnq => lnq.Descricao).NotEmpty().WithMessage("O campo descrição é obrigatório.");
            RuleFor(lnq => lnq.Numero).NotEmpty().WithMessage("O campo número é obrigatório.");
            RuleFor(lnq => lnq.CodigoBloco).NotEmpty().WithMessage("O campo código do bloco é obrigatório.");
            RuleFor(lnq => lnq.Numero).NotEmpty().WithMessage("O campo número é obrigatório.");

            When(lnq => lnq.CodigoBloco > 0, () => {
                RuleFor(lnq => lnq.CodigoBloco).MustAsync(ValidarSeBlocoExiste).WithMessage(c => $"Não foi encontrado um bloco com o código {c.CodigoBloco}.");
            });
        }

        private async Task<bool> ValidarSeBlocoExiste(int codigoBloco, CancellationToken cancellationToken)
        {
            return await _blocoRepositorio.Contem(lnq => lnq.Codigo == codigoBloco);
        }
    }
}
