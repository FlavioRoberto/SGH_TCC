using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Editar
{
    public class EditarCargoDisciplinaComandoValidador : CargoDisciplinaComandoValidadorBase<EditarCargoDisciplinaComando>, IValidador<EditarCargoDisciplinaComando>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;

        public EditarCargoDisciplinaComandoValidador(ICargoRepositorio cargoRepositorio,
                                                     ITurnoRepositorio turnoRepositorio,
                                                     ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                     ICargoDisciplinaRepositorio cargoDisciplinaRepositorio) :
                                                     base(cargoRepositorio, turnoRepositorio, curriculoDisciplinaRepositorio)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;

            When(lnq => lnq.CodigoCargo > 0 && lnq.CodigoCurriculoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq).MustAsync(ValidarSeCargoDisciplinaJaAdicionado).WithMessage("Já foi adicionado uma disciplina com os mesmos valores.");
            });
        }

        private async Task<bool> ValidarSeCargoDisciplinaJaAdicionado(EditarCargoDisciplinaComando comando, CancellationToken arg2)
        {
            var resultado = await _cargoDisciplinaRepositorio.Contem(lnq => lnq.Codigo != comando.CodigoCargo &&
                                                                     lnq.CodigoCargo == comando.CodigoCargo &&
                                                                     lnq.CodigoCurriculoDisciplina == comando.CodigoCurriculoDisciplina &&
                                                                     lnq.CodigoTurno == comando.CodigoTurno);
            return !resultado;
        }
    }
}
