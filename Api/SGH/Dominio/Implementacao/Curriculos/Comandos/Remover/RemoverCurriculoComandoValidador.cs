using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover
{
    public class RemoverCurriculoComandoValidador : AbstractValidator<RemoverCurriculoComando>, IRemoverCurriculoComandoValidador
    {
        private readonly ICurriculoRepositorio _repositorio;

        public RemoverCurriculoComandoValidador(ICurriculoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public RemoverCurriculoComandoValidador()
        {
            RuleFor(lnq => lnq.CodigoCurriculo).MustAsync(ValidarCurriculoExistente).WithMessage(comando => $"Não foi possível remover o currículo: Currículos com código {comando.CodigoCurriculo} não encontrado!");
        }

        private async Task<bool> ValidarCurriculoExistente(long Id, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Contem(lnq => lnq.Codigo == Id);
            return !resultado;
          }

    }
}
