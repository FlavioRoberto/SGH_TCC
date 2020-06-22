using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComandoValidador : CurriculoDisciplinaComandoBaseValidador<EditarCurriculoDisciplinaComando>, IValidador<EditarCurriculoDisciplinaComando>
    {
        public EditarCurriculoDisciplinaComandoValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                         IDisciplinaRepositorio disciplinaRepositorio,
                                                         ICurriculoRepositorio curriculoRepositorio) :
                                                         base(curriculoDisciplinaRepositorio, disciplinaRepositorio, curriculoRepositorio)
        {
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código da disciplina do currículo é obrigatório");

            When(lnq => lnq.Codigo > 0 && lnq.CodigoCurriculo > 0 && lnq.CodigoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq).MustAsync(ValidarSeDisciplinaCurriculoJaAdicionada).WithMessage(c => $"Disciplina já adicionada neste currículo.");
            });

            When(lnq => lnq.PreRequisitos != null && lnq.PreRequisitos.Any(), () =>
            {
                RuleFor(lnq => lnq.PreRequisitos).Must(ValidarSeCodigoDisciplinaCurriculoFoiInformado).WithMessage("Não foi informado o campo código da disciplina do currículo para algum pré-requisito.");
                RuleFor(lnq => lnq).Must(ValidarSePreRequisitosSaoDaDisciplina).WithMessage("Existem pré-requisitos com o código de disciplina do currículo diferente do selecionado.");
            });

        }

        private async Task<bool> ValidarSeDisciplinaCurriculoJaAdicionada(EditarCurriculoDisciplinaComando comando, CancellationToken arg2)
        {
            var resultado = await _curriculoDisciplinaRepositorio.Contem(lnq => lnq.CodigoDisciplina == comando.CodigoDisciplina &&
                                                                                lnq.CodigoCurriculo == comando.CodigoCurriculo &&
                                                                                lnq.Codigo != comando.Codigo);
            return !resultado;
        }

        private bool ValidarSeCodigoDisciplinaCurriculoFoiInformado(IEnumerable<DisciplinCurriculoPreRequisitoaViewModel> preRequisitos)
        {
            return !preRequisitos.Any(lnq => lnq.CodigoCurriculoDisciplina <= 0);
        }

        private bool ValidarSePreRequisitosSaoDaDisciplina(EditarCurriculoDisciplinaComando comando)
        {
            return !comando.PreRequisitos.Any(lnq => lnq.CodigoCurriculoDisciplina != comando.Codigo);
        }
    }
}
