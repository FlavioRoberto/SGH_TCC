using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComandoValidador : ProfessorComandoValidador<AtualizarProfessorComando>, IValidador<AtualizarProfessorComando>
    {
        public AtualizarProfessorComandoValidador(IProfessorRepositorio repositorio) : base(repositorio)
        {
            RuleFor(lnq => lnq).MustAsync(ValidarMatriculaExistente).WithMessage(comando => $"A matrícula {comando.Matricula} já está em uso.");
        }

        private async Task<bool> ValidarMatriculaExistente(AtualizarProfessorComando comando, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Listar(lnq => lnq.Matricula == comando.Matricula && lnq.Codigo != comando.ProfessorId);
            return resultado.Count > 0;
        }
    }
}
