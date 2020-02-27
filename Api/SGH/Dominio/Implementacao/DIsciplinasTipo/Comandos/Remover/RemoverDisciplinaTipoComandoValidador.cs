using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover
{
    public class RemoverDisciplinaTipoComandoValidador : AbstractValidator<RemoverDisciplinaTipoComando>, IRemoverDisciplinaTipoComandoValidador
    {
        private readonly IDisciplinaTipoRepositorio _repositorio;

        public RemoverDisciplinaTipoComandoValidador(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código não pode ser vazio.");
            RuleFor(lnq => lnq.Codigo).MustAsync(ValidarDisciplinaTipoExistente).WithMessage(disciplinaTipo => $"O tipo de disciplina de código {disciplinaTipo.Codigo} não foi encontrado.");
        }

        private async Task<bool> ValidarDisciplinaTipoExistente(int codigo, CancellationToken cancellationToken)
        {
            var disciplinaTipo = await _repositorio.Consultar(lnq => lnq.Codigo == codigo);

            if (disciplinaTipo == null)
                return false;

            return true;
        }
    }
}
