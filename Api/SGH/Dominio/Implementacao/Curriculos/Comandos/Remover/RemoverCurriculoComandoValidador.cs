using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover
{
    public class RemoverCurriculoComandoValidador : AbstractValidator<RemoverCurriculoComando>, IValidador<RemoverCurriculoComando>
    {
        private readonly ICurriculoRepositorio _repositorio;

        public RemoverCurriculoComandoValidador(ICurriculoRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.CodigoCurriculo).MustAsync(ValidarCurriculoExistente).WithMessage(comando => $"Não foi possível remover o currículo: Currículos com código {comando.CodigoCurriculo} não encontrado!");
            When(lnq => lnq.CodigoCurriculo > 0, () =>
            {
                RuleFor(lnq => lnq.CodigoCurriculo).MustAsync(VerificarSeCurriculoTemDisciplina).WithMessage("Não foi possível remover esse currículo pois tem disciplinas vinculadas a ele.");
            });
        }

        private async Task<bool> ValidarCurriculoExistente(long Id, CancellationToken cancellationToken)
        {
            return await _repositorio.Contem(lnq => lnq.Codigo == Id);
        }

        private async Task<bool> VerificarSeCurriculoTemDisciplina(long codigoCurriculo, CancellationToken arg2)
        {
            var quantidade = await _repositorio.RetornarQuantidadeDisciplinaCurriculo(codigoCurriculo);
            return quantidade > 0 ? false : true;
        }

    }
}
