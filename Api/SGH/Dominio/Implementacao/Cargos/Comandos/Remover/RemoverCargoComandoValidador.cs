using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Remover
{
    public class RemoverCargoComandoValidador : AbstractValidator<RemoverCargoComando>, IRemoverCargoComandoValidador
    {
        private readonly ICargoRepositorio _cargoRepositorio;

        public RemoverCargoComandoValidador(ICargoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O parâmetro código é obrigatório.");

            When(lnq => lnq.Codigo > 0, () =>
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeCargoExiste).WithMessage(c => $"Não foi encontrado um cargo com o código {c.Codigo}.")
            );
        }

        private async Task<bool> ValidarSeCargoExiste(int codigo, CancellationToken cancellationToken)
        {
            var resultado = await _cargoRepositorio.Contem(lnq => lnq.Codigo == codigo);
            return resultado;
        }
    }
}
