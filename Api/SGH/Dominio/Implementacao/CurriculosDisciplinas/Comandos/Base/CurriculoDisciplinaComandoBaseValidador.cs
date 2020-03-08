using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base
{
    public abstract class CurriculoDisciplinaComandoBaseValidador<T> : AbstractValidator<T> where T : CurriculoDisciplinaComandoBase
    {
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        private readonly IDisciplinaRepositorio _disciplinaRepositorio;

        public CurriculoDisciplinaComandoBaseValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio, IDisciplinaRepositorio disciplinaRepositorio)
        {
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
            _disciplinaRepositorio = disciplinaRepositorio;

            RuleFor(lnq => lnq.Periodo).NotEmpty().WithMessage("O campo período é obrigatório.");
            RuleFor(lnq => lnq.CodigoDisciplina).NotEmpty().WithMessage("O campo código da disciplina é obrigatório.");
            RuleFor(lnq => lnq.CodigoCurriculo).NotEmpty().WithMessage("O campo código do currículo é obrigatório.");
            RuleFor(lnq => lnq.AulasSemanaisTeorica).NotEmpty().WithMessage("O campo aulas semanais teóricas é obrigatório.");
            RuleFor(lnq => lnq.AulasSemanaisPratica).NotEmpty().WithMessage("O campo aulas semanais práticas é obrigatório.");
           
            When(lnq => lnq.CodigoDisciplina > 0, () => {
                RuleFor(lnq => lnq.CodigoDisciplina)
                    .MustAsync(ValidarSeDisciplinaExiste)
                    .WithMessage(c => $"Não foi encontrado uma disciplina com o código {c.CodigoDisciplina}.");
            });

            When(lnq => lnq.CodigoCurriculo > 0, () => {
                RuleFor(lnq => lnq.CodigoCurriculo)
                    .MustAsync(ValidarSeCurriculoExiste)
                    .WithMessage(c => $"Não foi encontrado um currículo com o código {c.CodigoCurriculo}.");
            });
        }

        private async Task<bool> ValidarSeCurriculoExiste(int codigoDisciplina, CancellationToken arg2)
        {
            return await _curriculoDisciplinaRepositorio.Contem(lnq => lnq.Codigo == codigoDisciplina);
        }

        private async Task<bool> ValidarSeDisciplinaExiste(int codigoDisciplina, CancellationToken arg2)
        {
            return await _disciplinaRepositorio.Contem(lnq => lnq.Codigo == codigoDisciplina);
        }
    }
}
