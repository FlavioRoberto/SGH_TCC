using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Remover
{
    public class RemoverCursoComandoValidador : AbstractValidator<RemoverCursoComando>, IRemoverCursoComandoValidador
    {

        private readonly ICursoRepositorio _repositorio;

        public RemoverCursoComandoValidador(ICursoRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.CursoId).NotEmpty().WithMessage("O campo código não pode ser vazio.");
            RuleFor(lnq => lnq.CursoId).MustAsync(ValidarCursoExistente).WithMessage(comando => $"Não foi encontrado o curso com código {comando.CursoId}.");
        }

        private async Task<bool> ValidarCursoExistente(int cursoId, CancellationToken cancellationToken)
        {
            var curso = await _repositorio.Consultar(lnq => lnq.Codigo == cursoId);

            if (curso == null)
                return false;

            return true;
        }
    }
}
