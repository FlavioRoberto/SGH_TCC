using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Criar
{
    public class CriarProfessorComandoValidador : ProfessorComandoValidador<CriarProfessorComando>, ICriarProfessorComandoValidador
    {
        public CriarProfessorComandoValidador(IProfessorRepositorio repositorio) : base(repositorio)
        {
            RuleFor(lnq => lnq.Matricula).MustAsync(ValidarMatriculaExistente).WithMessage(comando => $"A matrícula {comando.Matricula} já está em uso.");
        }

        private async Task<bool> ValidarMatriculaExistente(string matricula, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPor(lnq => lnq.Matricula == matricula);
            return resultado.Count > 0;
        }
    }
}
