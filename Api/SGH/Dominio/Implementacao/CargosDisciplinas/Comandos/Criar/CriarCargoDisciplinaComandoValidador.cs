using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar
{
    public class CriarCargoDisciplinaComandoValidador : CargoDisciplinaComandoValidadorBase<CriarCargoDisciplinaComando>, IValidador<CriarCargoDisciplinaComando>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;

        public CriarCargoDisciplinaComandoValidador(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                    ICargoRepositorio cargoRepositorio,
                                                    ITurnoRepositorio turnoRepositorio,
                                                    ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio) :
                                                    base(cargoRepositorio, turnoRepositorio, curriculoDisciplinaRepositorio)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;

            When(lnq => lnq.CodigoCargo > 0 && lnq.CodigoCurriculoDisciplina > 0, () =>
            {
                RuleFor(lnq => lnq).MustAsync(ValidarSeCargoDisciplinaJaAdicionado).WithMessage("Já foi adicionado uma disciplina com os mesmos valores.");
            });

        }

        private async Task<bool> ValidarSeCargoDisciplinaJaAdicionado(CriarCargoDisciplinaComando comando, CancellationToken arg2)
        {
            var resultado = await _cargoDisciplinaRepositorio.Contem(lnq => lnq.CodigoCargo == comando.CodigoCargo &&
                                                                     lnq.CodigoCurriculoDisciplina == comando.CodigoCurriculoDisciplina &&
                                                                     lnq.CodigoTurno == comando.CodigoTurno);
            return !resultado;
        }
    }
}
