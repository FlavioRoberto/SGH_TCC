using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Remover
{
    public class RemoverProfessorComandoValidador : AbstractValidator<RemoverProfessorComando>, IRemoverProfessorComandoValidador
    {
        private readonly IProfessorRepositorio _repositorio;

        public RemoverProfessorComandoValidador(IProfessorRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.ProfessorId).NotEmpty().WithMessage("O código do professor não pode ser vazio.");
            RuleFor(lnq => lnq.ProfessorId).MustAsync(ValidarProfessorExistente).WithMessage(lnq => $"Não foi encontrado um professor com o código {lnq.ProfessorId}.");
        }

        private async Task<bool> ValidarProfessorExistente(int professorId, CancellationToken cancellationToken)
        {
            return await _repositorio.Consultar(lnq => lnq.Codigo == professorId) != null;
        }
    }
}
