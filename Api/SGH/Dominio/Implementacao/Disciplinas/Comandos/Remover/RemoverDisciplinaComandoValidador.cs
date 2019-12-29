using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Disciplinas.Comandos.Remover
{
    public class RemoverDisciplinaComandoValidador : AbstractValidator<RemoverDisciplinaComando>, IRemoverDisciplinaComandoValidador
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public RemoverDisciplinaComandoValidador(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.CodigoDisciplina).NotEmpty().WithMessage("O código da disciplina não pode ser vazio.");
            RuleFor(lnq => lnq.CodigoDisciplina).MustAsync(ValidarDisciplinaExistente).WithMessage(comando => $"A disciplina de código {comando.CodigoDisciplina} não foi encontrada.");
        }

        private async Task<bool> ValidarDisciplinaExistente(long codigoDisciplina, CancellationToken cancellationToken)
        {
            var disciplina = await _repositorio.Listar(lnq => lnq.Codigo == codigoDisciplina);

            if (disciplina == null)
                return false;

            return true;
        }
    }
}
