using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar
{
    public class CriarAulaComandoValidador : AbstractValidator<CriarAulaComando>, IValidador<CriarAulaComando>
    {
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly IHorarioAulaRepositorio _horarioRepositorio;
        private readonly IDisciplinaRepositorio _disciplinaRepositorio;

        public CriarAulaComandoValidador(ISalaRepositorio salaRepositorio, 
                                         IHorarioAulaRepositorio horarioAulaRepositorio,
                                         IDisciplinaRepositorio disciplinaRepositorio)
        {
            _horarioRepositorio = horarioAulaRepositorio;
            _salaRepositorio = salaRepositorio;
            _disciplinaRepositorio = disciplinaRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.Reserva)
                .NotEmpty()
                .WithMessage("Não foi reservado horário e dia da semana para a aula")
                .DependentRules(() => {
                    RuleFor(lnq => lnq.Reserva.DiaSemana)
                        .NotEmpty()
                        .WithMessage("O Dia da semana não pode ser vazio.");

                    RuleFor(lnq => lnq.Reserva.Hora)
                        .NotEmpty()
                        .WithMessage("O campo hora não pode ser vazio.");
                });

            When(lnq => lnq.Desdobramento, () =>
            {
                RuleFor(lnq => lnq.DescricaoDesdobramento)
                    .NotEmpty()
                    .WithMessage("Não foi informada uma descrição para o desdobramento.");
            });

            RuleFor(lnq => lnq.CodigoSala)
                .NotEmpty()
                .WithMessage("O código da sala não pode ser vazio.")

                .MustAsync(ValidarSeSalaExiste)
                .WithMessage(c => $"Não foi encontrada uma sala com o código {c.CodigoSala}.");

            RuleFor(lnq => lnq.CodigoHorario)
                .NotEmpty()
                .WithMessage("O código do horário não pode ser vazio.")
                
                .MustAsync(ValidarSeHorarioExiste)
                .WithMessage(c => $"Não foi encontrado um horário com o código {c.CodigoHorario}");

            RuleFor(lnq => lnq.CodigoDisciplina)
                .NotEmpty()
                .WithMessage("O código da disciplina não pode ser vazio.")
                
                .MustAsync(ValidarSeDisciplinaExiste)
                .WithMessage(c => $"Não foi encontrada uma disciplina de cargo com o código {c.CodigoDisciplina}.");
        }

        private async Task<bool> ValidarSeSalaExiste(int codigoSala, CancellationToken arg2)
        {
            return await _salaRepositorio.Contem(lnq => lnq.Codigo == codigoSala);
        }

        private async Task<bool> ValidarSeHorarioExiste(int codigoHorario, CancellationToken arg2)
        {
            return await _horarioRepositorio.Contem(lnq => lnq.Codigo == codigoHorario);
        }

        private async Task<bool> ValidarSeDisciplinaExiste(int codigoDisciplina, CancellationToken arg2)
        {
            return await _disciplinaRepositorio.Contem(lnq => lnq.Codigo == codigoDisciplina);
        }
    }
}
