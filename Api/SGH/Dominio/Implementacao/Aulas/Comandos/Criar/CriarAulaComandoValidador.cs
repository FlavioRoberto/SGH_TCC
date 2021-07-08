using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar
{
    public class CriarAulaComandoValidador : AulaComandoBaseValidador<CriarAulaComando>
    {
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ICargoRepositorio _cargoRepositorio;

        public CriarAulaComandoValidador(ISalaRepositorio salaRepositorio,
                                         IHorarioAulaRepositorio horarioAulaRepositorio,
                                         IAulaRepositorio aulaRepositorio,
                                         ICargoRepositorio cargoRepositorio,
                                         ICargoDisciplinaRepositorio cargoDisciplinaRepositorio)
                                         : base(salaRepositorio, horarioAulaRepositorio, cargoDisciplinaRepositorio)
        {
            _aulaRepositorio = aulaRepositorio;
            _cargoRepositorio = cargoRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.Reserva)
                .NotEmpty()
                .WithMessage("Não foi reservado horário e dia da semana para a aula")
                .DependentRules(() =>
                {
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

            When(ValidarSeCamposComandoForamInformados, () =>
            {
                RuleFor(lnq => lnq)

                   .MustAsync(ValidarSeHorarioDisponivel)
                   .WithMessage(x => $"Não foi possível criar a aula para {x.Reserva.DiaSemana} às {x.Reserva.Hora}h, pois já tem uma aula reservada para esse dia e horário.")

                   .MustAsync(ValidarSeCargoDisponivel)
                   .WithMessage(x => $"Não foi possível criar a aula, pois o cargo selecionado já está reservado para {x.Reserva.DiaSemana} às {x.Reserva.Hora}h.")

                   .MustAsync(ValidarSeProfessorDisponivel)
                   .WithMessage(x => $"Não foi possível criar a aula, pois o professor selecionado já está reservado para {x.Reserva.DiaSemana} às {x.Reserva.Hora}h.")

                   .MustAsync(ValidarSeSalaDisponivel)
                   .WithMessage(x => $"Não foi possível criar a aula, pois a sala selecionada já está reservada para {x.Reserva.DiaSemana} às {x.Reserva.Hora}h.");
            });

        }

        private bool ValidarSeCamposComandoForamInformados(CriarAulaComando comando)
        {
            return comando.CodigoSala > 0 &&
                   comando.CodigoDisciplina > 0 &&
                   comando.CodigoSala > 0 &&
                   comando.Reserva != null &&
                   !string.IsNullOrEmpty(comando.Reserva.DiaSemana) &&
                   !string.IsNullOrEmpty(comando.Reserva.Hora);
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

            if (cargoDisciplina == null)
                return false;

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
    }
}
