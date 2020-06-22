using FluentValidation;
using SGH.Dominio.Core.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Base
{
    public abstract class CargoComandoValidadorBase<T> : AbstractValidator<T> where T : ICargoComando
    {
        private readonly IProfessorRepositorio _professorRepositorio;

        public CargoComandoValidadorBase(IProfessorRepositorio professorRepositorio)
        {
            _professorRepositorio = professorRepositorio;

            RuleFor(lnq => lnq.Ano).NotEmpty().WithMessage("O campo ano é obrigatório.");
            RuleFor(lnq => lnq.Edital).NotEmpty().WithMessage("O campo edital é obrigatório.");
            RuleFor(lnq => lnq.Numero).NotEmpty().WithMessage("O campo número é obrigatório.");
            RuleFor(lnq => lnq.Semestre).NotEmpty().WithMessage("O campo semestre é obrigatório.");

            When(lnq => lnq.CodigoProfessor.HasValue, () => {
                RuleFor(lnq => lnq.CodigoProfessor).MustAsync(ValidarSeProfessorExiste).WithMessage(c => $"Não foi encontrado um professor com o código {c.CodigoProfessor}.");
            });
        }

        private async Task<bool> ValidarSeProfessorExiste(int? codigoProfessor, CancellationToken cancellationToken)
        {
            var resultado = await _professorRepositorio.Contem(lnq => lnq.Codigo == codigoProfessor.Value && lnq.Ativo == true);
            return resultado;
        }

    }
}
