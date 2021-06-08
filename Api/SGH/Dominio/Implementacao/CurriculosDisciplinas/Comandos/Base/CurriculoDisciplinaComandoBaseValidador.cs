using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base
{
    public abstract class CurriculoDisciplinaComandoBaseValidador<T> : AbstractValidator<T> where T : CurriculoDisciplinaComandoBase
    {
        protected readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        protected readonly IDisciplinaRepositorio _disciplinaRepositorio;
        protected readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly IDisciplinaTipoRepositorio _disciplinaTipoRepositorio;

        public CurriculoDisciplinaComandoBaseValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                       IDisciplinaRepositorio disciplinaRepositorio,
                                                       ICurriculoRepositorio curriculoRepositorio,
                                                       IDisciplinaTipoRepositorio disciplinaTipoRepositorio)
        {
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
            _disciplinaRepositorio = disciplinaRepositorio;
            _curriculoRepositorio = curriculoRepositorio;
            _disciplinaTipoRepositorio = disciplinaTipoRepositorio;

            RuleFor(lnq => lnq.Periodo).NotEmpty().WithMessage("O campo período é obrigatório.");
            RuleFor(lnq => lnq.CodigoDisciplina).NotEmpty().WithMessage("O campo código da disciplina é obrigatório.");
            RuleFor(lnq => lnq.CodigoCurriculo).NotEmpty().WithMessage("O campo código do currículo é obrigatório.");
            RuleFor(lnq => lnq.AulasSemanaisTeorica).GreaterThanOrEqualTo(0).WithMessage("O campo aulas semanais teóricas é obrigatório.");
            RuleFor(lnq => lnq.AulasSemanaisPratica).GreaterThanOrEqualTo(0).WithMessage("O campo aulas semanais práticas é obrigatório.");
            RuleFor(lnq => lnq.DisciplinaTipo).NotEmpty().GreaterThan(0).WithMessage("O campo disciplina tipo é obrigatório.");

            When(lnq => lnq.CodigoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq.CodigoDisciplina)
                    .MustAsync(ValidarSeDisciplinaExiste)
                    .WithMessage(c => $"Não foi encontrado uma disciplina com o código {c.CodigoDisciplina}.");
            });

            When(lnq => lnq.CodigoCurriculo > 0, () =>
            {
                RuleFor(lnq => lnq.CodigoCurriculo)
                    .MustAsync(ValidarSeCurriculoExiste)
                    .WithMessage(c => $"Não foi encontrado um currículo com o código {c.CodigoCurriculo}.");
            });

            When(lnq => lnq.DisciplinaTipo > 0, () =>
            {
                RuleFor(lnq => lnq.DisciplinaTipo)
                    .MustAsync(ValidarSeTipoDisciplinaExiste)
                    .WithMessage(c => $"Não foi encontrado um tipo de disciplina com o código {c.DisciplinaTipo}.");
            });

        }

        private async Task<bool> ValidarSeCurriculoExiste(int codigoCurriculo, CancellationToken arg2)
        {
            return await _curriculoRepositorio.Contem(lnq => lnq.Codigo == codigoCurriculo);
        }

        private async Task<bool> ValidarSeDisciplinaExiste(int codigoDisciplina, CancellationToken arg2)
        {
            return await _disciplinaRepositorio.Contem(lnq => lnq.Codigo == codigoDisciplina);
        }

        private async Task<bool> ValidarSeTipoDisciplinaExiste(int codigoDisciplinaTipo, CancellationToken arg2)
        {
            return await _disciplinaTipoRepositorio.Contem(lnq => lnq.Codigo == codigoDisciplinaTipo);
        }
    }
}
