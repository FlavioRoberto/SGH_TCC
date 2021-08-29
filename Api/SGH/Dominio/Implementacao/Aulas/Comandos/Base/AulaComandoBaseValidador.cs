using FluentValidation;
using SGH.Dominio.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Base
{
    public abstract class AulaComandoBaseValidador<T> : Validador<T> where T : AulaComandoBase
    {
        protected readonly ISalaRepositorio _salaRepositorio;
        protected readonly IHorarioAulaRepositorio _horarioRepositorio;
        protected readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;

        public AulaComandoBaseValidador(ISalaRepositorio salaRepositorio,
                                        IHorarioAulaRepositorio horarioAulaRepositorio,
                                        ICargoDisciplinaRepositorio cargoDisciplinaRepositorio)
        {
            _salaRepositorio = salaRepositorio;
            _horarioRepositorio = horarioAulaRepositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            //RuleFor(lnq => lnq.CodigoSala)
            //    .NotEmpty()
            //    .WithMessage("O código da sala não pode ser vazio.");

            RuleFor(lnq => lnq.CodigoHorario)
                .NotEmpty()
                .WithMessage("O código do horário não pode ser vazio.");

            RuleFor(lnq => lnq.CodigoDisciplina)
                .NotEmpty()
                .WithMessage("O código da disciplina não pode ser vazio.");

            When(lnq => lnq.CodigoSala.HasValue, () =>
            {
                RuleFor(lnq => lnq)
                 .MustAsync(ValidarSeSalaExiste)
                 .WithMessage(c => $"Não foi encontrada uma sala com o código {c.CodigoSala}.");
            });

            When(ValidarSeCamposComandoForamInformados, () =>
            {
                RuleFor(lnq => lnq)
                   //  .MustAsync(ValidarSeSalaExiste)
                   //  .WithMessage(c => $"Não foi encontrada uma sala com o código {c.CodigoSala}.")

                   .MustAsync(ValidarSeHorarioExiste)
                   .WithMessage(c => $"Não foi encontrado um horário com o código {c.CodigoHorario}")

                   .MustAsync(ValidarSeDisciplinaExiste)
                   .WithMessage(c => $"Não foi encontrada uma disciplina de cargo com o código {c.CodigoDisciplina}.");
            });
        }

        private bool ValidarSeCamposComandoForamInformados(AulaComandoBase comando)
        {
            return comando.CodigoDisciplina > 0 &&
                   comando.CodigoSala > 0;
        }

        private async Task<bool> ValidarSeSalaExiste(AulaComandoBase comando, CancellationToken arg2)
        {
            return await _salaRepositorio.Contem(lnq => lnq.Codigo == comando.CodigoSala);
        }

        private async Task<bool> ValidarSeHorarioExiste(AulaComandoBase comando, CancellationToken arg2)
        {
            return await _horarioRepositorio.Contem(lnq => lnq.Codigo == comando.CodigoHorario);
        }

        private async Task<bool> ValidarSeDisciplinaExiste(AulaComandoBase comando, CancellationToken arg2)
        {
            return await _cargoDisciplinaRepositorio.Contem(lnq => lnq.Codigo == comando.CodigoDisciplina);
        }
    }
}
