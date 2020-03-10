using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using SGH.Dominio.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComandoValidador : CurriculoDisciplinaComandoBaseValidador<EditarCurriculoDisciplinaComando>, IEditarCurriculoDisciplinaComandoValidador
    {
        public EditarCurriculoDisciplinaComandoValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio, IDisciplinaRepositorio disciplinaRepositorio) : base(curriculoDisciplinaRepositorio, disciplinaRepositorio)
        {
            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código é obrigatório");

            When(lnq => lnq.PreRequisitos.Any(), () =>
            {
                RuleFor(lnq => lnq.PreRequisitos).Must(ValidarSeCodigoDisciplinaCurriculoFoiInformado).WithMessage("Não foi informado o campo código da disciplina do currículo para algum pré-requisito.");
                RuleFor(lnq => lnq).Must(ValidarSePreRequisitosSaoDaDisciplina).WithMessage("Existem pré-requisitos com o código de disciplina do currículo diferente do selecionado.");
            });

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
