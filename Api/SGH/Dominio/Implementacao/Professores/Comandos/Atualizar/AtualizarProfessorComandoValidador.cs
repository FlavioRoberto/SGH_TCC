using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComandoValidador : ProfessorComandoValidador<AtualizarProfessorComando>
    {
        public AtualizarProfessorComandoValidador(IProfessorRepositorio repositorio) : base(repositorio)
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.Codigo)
                .NotEmpty()
                .WithMessage("O código não foi informado")
                .GreaterThan(0)
                .WithMessage("O código deve ser maior que 0.");

            RuleFor(lnq => lnq).MustAsync(ValidarMatriculaExistente).WithMessage(comando => $"A matrícula {comando.Matricula} já está em uso.");
        }

        private async Task<bool> ValidarMatriculaExistente(AtualizarProfessorComando comando, CancellationToken cancellationToken)
        {
            var matriculaExiste = await _repositorio.Contem(lnq => lnq.Matricula == comando.Matricula && lnq.Codigo != comando.Codigo);

            if (matriculaExiste)
                return false;

            return true;
        }
    }
}
