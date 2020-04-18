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
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;

        public RemoverCurriculoComandoValidador(ICurriculoRepositorio repositorio, IHorarioAulaRepositorio horarioAulaRepositorio)
        {
            _repositorio = repositorio;
            _horarioAulaRepositorio = horarioAulaRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.CodigoCurriculo)
                .MustAsync(ValidarCurriculoExistente)
                .WithMessage(comando => $"Não foi possível remover o currículo: Currículos com código {comando.CodigoCurriculo} não encontrado!")
               
                .MustAsync(ValidarSeCurriculoTemDisciplina)
                .WithMessage("Não foi possível remover esse currículo pois tem disciplinas vinculadas a ele.")
                
                .MustAsync(ValidarSeCurriculoVinculadoEmHorarios)
                .WithMessage("Não foi possível remover esse currículo pois ele está vinculado a horários.");          
        }

        private async Task<bool> ValidarCurriculoExistente(long Id, CancellationToken cancellationToken)
        {
            return await _repositorio.Contem(lnq => lnq.Codigo == Id);
        }

        private async Task<bool> ValidarSeCurriculoTemDisciplina(long codigoCurriculo, CancellationToken arg2)
        {
            var quantidade = await _repositorio.RetornarQuantidadeDisciplinaCurriculo(codigoCurriculo);
            return quantidade > 0 ? false : true;
        }

        private async Task<bool> ValidarSeCurriculoVinculadoEmHorarios(long codigoCurriculo, CancellationToken arg2)
        {
            var vinculado = await _horarioAulaRepositorio.Contem(lnq => lnq.CodigoCurriculo == codigoCurriculo);

            if (vinculado)
                return false;

            return true;
        }

    }
}
