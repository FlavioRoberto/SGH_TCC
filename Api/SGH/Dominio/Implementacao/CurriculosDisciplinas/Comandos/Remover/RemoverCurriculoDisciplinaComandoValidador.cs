using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Remover
{
    public class RemoverCurriculoDisciplinaComandoValidador : Validador<RemoverCurriculoDisciplinaComando>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        private CargoDisciplina _cargo;

        public RemoverCurriculoDisciplinaComandoValidador(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;

            RuleFor(lnq => lnq.Codigo).NotEmpty().WithMessage("O campo código não pode ser vazio.");

            When(lnq => lnq.Codigo > 0, () =>
            {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeDisciplinaExiste).WithMessage(c => $"Não foi encontrado uma disciplina do currículo com código {c.Codigo}.");
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeDisciplinaVinculadaEmCargo).WithMessage(c => $"Não foi possível remover a disciplina pois ela está vinculada ao cargo de código {_cargo.CodigoCargo}.");
            });
        }

        private async Task<bool> ValidarSeDisciplinaVinculadaEmCargo(int codigo, CancellationToken arg2)
        {
            _cargo = await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.CodigoCurriculoDisciplina == codigo);
            return _cargo == null ? true : false;
        }

        private async Task<bool> ValidarSeDisciplinaExiste(int codigo, CancellationToken arg2)
        {
            return await _curriculoDisciplinaRepositorio.Contem(lnq => lnq.Codigo == codigo);
        }
    }
}
