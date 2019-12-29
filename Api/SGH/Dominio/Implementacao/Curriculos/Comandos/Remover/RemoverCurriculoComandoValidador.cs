using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Remover
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
            var curriculo = await _repositorio.ListarPor(lnq => lnq.Codigo == Id);

            if (curriculo == null)
                return false;

            return true;
        }

    }
}
