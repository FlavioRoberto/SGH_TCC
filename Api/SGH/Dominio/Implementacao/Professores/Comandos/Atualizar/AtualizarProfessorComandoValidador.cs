using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComandoValidador : ProfessorComandoValidador<AtualizarProfessorComando>, IAtualizarProfessorComandoValidador
    {
        public AtualizarProfessorComandoValidador(IProfessorRepositorio repositorio) : base(repositorio)
        {
            RuleFor(lnq => lnq).MustAsync(ValidarMatriculaExistente).WithMessage(comando => $"A matrícula {comando.Matricula} já está em uso.");
        }

        private async Task<bool> ValidarMatriculaExistente(AtualizarProfessorComando comando, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPor(lnq => lnq.Matricula == comando.Matricula && lnq.Codigo != comando.ProfessorId);
            return resultado.Count > 0;
        }
    }
}
