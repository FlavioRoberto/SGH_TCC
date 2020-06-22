using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover
{
    public class RemoverProfessorComandoValidador : AbstractValidator<RemoverProfessorComando>, IValidador<RemoverProfessorComando>
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly ICargoRepositorio _cargoRepositorio;
        private Cargo _cargo;

        public RemoverProfessorComandoValidador(IProfessorRepositorio repositorio,
                                                ICargoRepositorio cargoRepositorio)
        {
            _repositorio = repositorio;
            _cargoRepositorio = cargoRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.ProfessorId)
                .NotEmpty()
                .WithMessage("O código do professor não pode ser vazio.")
                
                .MustAsync(ValidarProfessorExistente)
                .WithMessage(lnq => $"Não foi encontrado um professor com o código {lnq.ProfessorId}.")
                
                .MustAsync(ValidarSeProfessorVinculadoCargo)
                .WithMessage(c => $"Não foi possível remover esse professor, pois ele está vinculado ao cargo de código {_cargo.Codigo}.");
        }

        private async Task<bool> ValidarSeProfessorVinculadoCargo(int codigoProfessor, CancellationToken arg2)
        {
            _cargo = await _cargoRepositorio.Consultar(lnq => lnq.CodigoProfessor == codigoProfessor);

            if (_cargo != null)
                return false;

            return true;
        }

        private async Task<bool> ValidarProfessorExistente(int professorId, CancellationToken cancellationToken)
        {
            return await _repositorio.Consultar(lnq => lnq.Codigo == professorId) != null;
        }
    }
}
