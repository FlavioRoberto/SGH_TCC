using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Remover
{
    public class RemoverDisciplinaComandoValidador : AbstractValidator<RemoverDisciplinaComando>, IValidador<RemoverDisciplinaComando>
    {
        private readonly IDisciplinaRepositorio _repositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;

        private CurriculoDisciplina _curriculoDisciplina;
        private CurriculoDisciplinaPreRequisito _preRequisito;

        public RemoverDisciplinaComandoValidador(IDisciplinaRepositorio repositorio,
                                                 ICurriculoRepositorio curriculoRepositorio,
                                                 ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio)
        {
            _repositorio = repositorio;
            _curriculoRepositorio = curriculoRepositorio;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;

            RuleFor(lnq => lnq.CodigoDisciplina).NotEmpty().WithMessage("O código da disciplina não pode ser vazio.");
            RuleFor(lnq => lnq.CodigoDisciplina).MustAsync(ValidarDisciplinaExistente).WithMessage(comando => $"A disciplina de código {comando.CodigoDisciplina} não foi encontrada.");

            When(lnq => lnq.CodigoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq.CodigoDisciplina).MustAsync(ValidarSeDisciplinaVinculadaAoCurriculo).WithMessage(c => $"Não foi possível remover esta disciplina, pois ela está vinculada ao currículo de código {_curriculoDisciplina.CodigoCurriculo}.");
                RuleFor(lnq => lnq.CodigoDisciplina).MustAsync(ValidarSeDisciplinaVinculadaEmPreRequisito).WithMessage(c => $"Não foi possível remover esta disciplina pois ela está vinculada como pré-requisito da disciplina {_curriculoDisciplina.Disciplina.Descricao} do currículo de código {_curriculoDisciplina.CodigoCurriculo}.");
            });
        
        }

        private async Task<bool> ValidarSeDisciplinaVinculadaAoCurriculo(long codigoDisciplina, CancellationToken arg2)
        {
            _curriculoDisciplina = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.CodigoDisciplina == codigoDisciplina);
            return _curriculoDisciplina == null ? true : false;
        }

        private async Task<bool> ValidarSeDisciplinaVinculadaEmPreRequisito(long codigoDisciplina, CancellationToken arg2)
        {
            _preRequisito = await _curriculoDisciplinaRepositorio.ConsultarPreRequisito(codigoDisciplina);

            if (_preRequisito == null)
                return true;

            _curriculoDisciplina = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == _preRequisito.CodigoDisciplina);

            return _curriculoDisciplina == null ? true : false;
        }
        
        private async Task<bool> ValidarDisciplinaExistente(long codigoDisciplina, CancellationToken cancellationToken)
        {
            var disciplina = await _repositorio.Consultar(lnq => lnq.Codigo == codigoDisciplina);

            if (disciplina == null)
                return false;

            return true;
        }
    }
}
