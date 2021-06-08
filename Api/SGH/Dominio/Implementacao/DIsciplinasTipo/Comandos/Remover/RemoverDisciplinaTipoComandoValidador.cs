using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover
{
    public class RemoverDisciplinaTipoComandoValidador : AbstractValidator<RemoverDisciplinaTipoComando>, IValidador<RemoverDisciplinaTipoComando>
    {
        private readonly IDisciplinaTipoRepositorio _repositorio;
        private readonly ICurriculoDisciplinaRepositorio _disciplinaRepositorio;
        private CurriculoDisciplina _disciplina;

        public RemoverDisciplinaTipoComandoValidador(IDisciplinaTipoRepositorio repositorio, ICurriculoDisciplinaRepositorio disciplinaRepositorio)
        {
            _repositorio = repositorio;
            _disciplinaRepositorio = disciplinaRepositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código não pode ser vazio.");
            RuleFor(lnq => lnq.Codigo).MustAsync(ValidarDisciplinaTipoExistente).WithMessage(disciplinaTipo => $"O tipo de disciplina de código {disciplinaTipo.Codigo} não foi encontrado.");

            When(lnq => lnq.Codigo > 0, () => {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeTipoDisciplinaVinculadoDisciplina).WithMessage(c => $"Não foi possível remover o tipo de disciplina pois esse tipo está vinculado a disciplina de código {_disciplina.Codigo}.");
            });
        }

        private async Task<bool> ValidarSeTipoDisciplinaVinculadoDisciplina(long codigoTipo, CancellationToken arg2)
        {
            _disciplina = await _disciplinaRepositorio.Consultar(lnq => lnq.CodigoTipo == codigoTipo);
            return _disciplina == null ? true : false;
        }

        private async Task<bool> ValidarDisciplinaTipoExistente(long codigo, CancellationToken cancellationToken)
        {
            var disciplinaTipo = await _repositorio.Consultar(lnq => lnq.Codigo == codigo);

            if (disciplinaTipo == null)
                return false;

            return true;
        }
    }
}
