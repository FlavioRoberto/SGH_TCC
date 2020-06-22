using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Criar
{
    public class CriarProfessorComandoValidador : ProfessorComandoValidador<CriarProfessorComando>, IValidador<CriarProfessorComando>
    {
        public CriarProfessorComandoValidador(IProfessorRepositorio repositorio) : base(repositorio)
        {
            RuleFor(lnq => lnq.Matricula).MustAsync(ValidarMatriculaExistente).WithMessage(comando => $"A matrícula {comando.Matricula} já está em uso.");
        }

        private async Task<bool> ValidarMatriculaExistente(string matricula, CancellationToken cancellationToken)
        {
            var contemMatricula = await _repositorio.Contem(lnq => lnq.Matricula == matricula);
            return !contemMatricula;
        }
    }
}
