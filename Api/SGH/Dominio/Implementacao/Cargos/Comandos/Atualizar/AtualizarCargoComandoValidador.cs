using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar
{
    public class AtualizarCargoComandoValidador : CargoComandoValidadorBase<AtualizarCargoComando>, IValidador<AtualizarCargoComando>
    {
        private ICargoRepositorio _cargoRepositorio;

        public AtualizarCargoComandoValidador(IProfessorRepositorio professorRepositorio, ICargoRepositorio cargoRepositorio) : base(professorRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;

            When(lnq => lnq.Codigo.HasValue, () =>
            {
                RuleFor(lnq => lnq.Codigo).MustAsync(ValidarSeCargoExiste).WithMessage(c => $"Não foi encontrado um cargo com o código {c.Codigo}.");
                RuleFor(lnq => lnq).MustAsync(ValidarSeCargoJaCadastrado).WithMessage("Já existe um cargo com os mesmos valores para os campos semestre, ano, edital e número.");
            });
        }

        private async Task<bool> ValidarSeCargoExiste(int? codigoCargo, CancellationToken arg2)
        {
            return await _cargoRepositorio.Contem(lnq => lnq.Codigo == codigoCargo.Value);
        }

        private async Task<bool> ValidarSeCargoJaCadastrado(AtualizarCargoComando comando, CancellationToken arg2)
        {
            var resultado = await _cargoRepositorio.Contem(lnq => lnq.Semestre == comando.Semestre &&
                                                           lnq.Ano == comando.Ano &&
                                                           lnq.Edital == comando.Edital &&
                                                           lnq.Numero == comando.Numero &&
                                                           lnq.Codigo != comando.Codigo);
            return !resultado;
        }
    }
}
