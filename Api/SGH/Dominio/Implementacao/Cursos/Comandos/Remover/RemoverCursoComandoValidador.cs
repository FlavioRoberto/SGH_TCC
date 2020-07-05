using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Remover
{
    public class RemoverCursoComandoValidador : AbstractValidator<RemoverCursoComando>, IValidador<RemoverCursoComando>
    {
        private readonly ICursoRepositorio _repositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private Curriculo _curriculo;

        public RemoverCursoComandoValidador(ICursoRepositorio repositorio,
                                            ICurriculoRepositorio curriculoRepositorio,
                                            IUsuarioRepositorio usuarioRepositorio)
        {
            _repositorio = repositorio;
            _curriculoRepositorio = curriculoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(lnq => lnq.CursoId)
                .NotEmpty()
                .WithMessage("O campo código não pode ser vazio.")

                .MustAsync(ValidarCursoExistente)
                .WithMessage(comando => $"Não foi encontrado o curso com código {comando.CursoId}.")

                .MustAsync(VerificarSeCursoVinculadoAoCurriculo)
                .WithMessage(c => $"Não foi possível remover o curso pois ele está vinculado ao currículo de código {_curriculo.Codigo}.")

                .MustAsync(VerificarSePossuiUsuarioVinculadoAoCurso)
                .WithMessage(c => "Não foi possível remover o curso, pois existem usuários vinculados ao mesmo.");

        }

        private async Task<bool> VerificarSePossuiUsuarioVinculadoAoCurso(int codigoCurso, CancellationToken arg2)
        {
            var existeUsuarioVinculado = await _usuarioRepositorio.Contem(lnq => lnq.CursoCodigo == codigoCurso);

            if (existeUsuarioVinculado)
                return false;

            return true;
        }

        private async Task<bool> VerificarSeCursoVinculadoAoCurriculo(int codigoCurso, CancellationToken arg2)
        {
            _curriculo = await _curriculoRepositorio.Consultar(lnq => lnq.CodigoCurso == codigoCurso);

            if (_curriculo != null)
                return false;

            return true;
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
