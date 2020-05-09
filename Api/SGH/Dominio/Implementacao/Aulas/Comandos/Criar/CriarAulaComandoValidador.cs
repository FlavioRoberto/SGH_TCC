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
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ICargoRepositorio _cargoRepositorio;

        public CriarAulaComandoValidador(ISalaRepositorio salaRepositorio, 
                                         IHorarioAulaRepositorio horarioAulaRepositorio,
                                         IAulaRepositorio aulaRepositorio,
                                         ICargoRepositorio cargoRepositorio,
                                         ICargoDisciplinaRepositorio cargoDisciplinaRepositorio)
        {
            _horarioRepositorio = horarioAulaRepositorio;
            _salaRepositorio = salaRepositorio;
            _aulaRepositorio = aulaRepositorio;
            _cargoRepositorio = cargoRepositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;

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
                .WithMessage("O código da sala não pode ser vazio.");

            RuleFor(lnq => lnq.CodigoHorario)
                .NotEmpty()
                .WithMessage("O código do horário não pode ser vazio.");     

            RuleFor(lnq => lnq.CodigoDisciplina)
                .NotEmpty()
                .WithMessage("O código da disciplina não pode ser vazio.");

            When(ValidarSeCamposComandoForamInformados, () =>
            {
                RuleFor(lnq => lnq)
                   .MustAsync(ValidarSeSalaExiste)
                   .WithMessage(c => $"Não foi encontrada uma sala com o código {c.CodigoSala}.")

                   .MustAsync(ValidarSeHorarioExiste)
                   .WithMessage(c => $"Não foi encontrado um horário com o código {c.CodigoHorario}")

                   .MustAsync(ValidarSeDisciplinaExiste)
                   .WithMessage(c => $"Não foi encontrada uma disciplina de cargo com o código {c.CodigoDisciplina}.")

                   .MustAsync(ValidarSeHorarioDisponivel)
                   .WithMessage("Não foi possível criar a aula nesse horário, pois já tem uma aula reservada para esse dia e horário.")

                   .MustAsync(ValidarSeCargoDisponivel)
                   .WithMessage("Não foi possível criar a aula, pois o cargo selecionado já está reservado para esse dia e horário.")

                   .MustAsync(ValidarSeProfessorDisponivel)
                   .WithMessage("Não foi possível criar a aula, pois o professor selecionado já está reservado para esse dia e horário.")

                   .MustAsync(ValidarSeSalaDisponivel)
                   .WithMessage("Não foi possível criar a aula, pois a sala selecionada já está reservada para esse dia e horário.");
            });
        }

        private bool ValidarSeCamposComandoForamInformados(CriarAulaComando comando)
        {
            return comando.Reserva != null &&
                  !string.IsNullOrEmpty(comando.Reserva.DiaSemana) &&
                  !string.IsNullOrEmpty(comando.Reserva.Hora) &&
                  comando.CodigoSala > 0 &&
                  comando.CodigoDisciplina > 0 &&
                  comando.CodigoSala > 0;
        }

        private async Task<bool> ValidarSeSalaDisponivel(CriarAulaComando comando, CancellationToken arg2)
        {
            var aulaReservada = await _aulaRepositorio.Contem(lnq => lnq.CodigoSala == comando.CodigoSala &&
                                                                     lnq.Reserva.Hora == comando.Reserva.Hora &&
                                                                     lnq.Reserva.DiaSemana == comando.Reserva.DiaSemana);
            if (aulaReservada)
                return false;

            return true;
        }

        private async Task<bool> ValidarSeProfessorDisponivel(CriarAulaComando comando, CancellationToken arg2)
        {
            var cargoDisciplina = await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == comando.CodigoDisciplina);

            var cargo = await _cargoRepositorio.Consultar(lnq => lnq.Codigo == cargoDisciplina.CodigoCargo);

            if (!cargo.CodigoProfessor.HasValue)
                return true;

            var aulaReservada = await _aulaRepositorio.VerificarDisponibilidadeProfessor(cargo.CodigoProfessor.Value, comando.Reserva.DiaSemana, comando.Reserva.Hora);

            if (aulaReservada)
                return false;

            return true;
        }

        private async Task<bool> ValidarSeCargoDisponivel(CriarAulaComando comando, CancellationToken arg2)
        {
            var cargoDisciplina = await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == comando.CodigoDisciplina);

            var aulaReservada = await _aulaRepositorio.VerificarDisponibilidadeCargo(cargoDisciplina.CodigoCargo, comando.Reserva.DiaSemana, comando.Reserva.Hora);

            if (aulaReservada)
                return false;

            return true;
        }

        private async Task<bool> ValidarSeHorarioDisponivel(CriarAulaComando comando, CancellationToken arg2)
        {
            var aulaReservada = await _aulaRepositorio.Contem(lnq => lnq.CodigoHorario == comando.CodigoHorario &&
                                                                     lnq.Reserva.DiaSemana == comando.Reserva.DiaSemana && 
                                                                     lnq.Reserva.Hora == comando.Reserva.Hora);

            if (aulaReservada)
                return false;

            return true;
        }

        private async Task<bool> ValidarSeSalaExiste(CriarAulaComando comando, CancellationToken arg2)
        {
            return await _salaRepositorio.Contem(lnq => lnq.Codigo == comando.CodigoSala);
        }

        private async Task<bool> ValidarSeHorarioExiste(CriarAulaComando comando, CancellationToken arg2)
        {
            return await _horarioRepositorio.Contem(lnq => lnq.Codigo == comando.CodigoHorario);
        }

        private async Task<bool> ValidarSeDisciplinaExiste(CriarAulaComando comando, CancellationToken arg2)
        {
            return await _cargoDisciplinaRepositorio.Contem(lnq => lnq.Codigo == comando.CodigoDisciplina);
        }
    }
}
