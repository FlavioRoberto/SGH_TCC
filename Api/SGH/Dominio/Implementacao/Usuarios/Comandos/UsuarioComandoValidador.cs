using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Shared.Extensions;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos
{
    public abstract class UsuarioComandoValidador<T> : AbstractValidator<T> where T : IUsuarioComando
    {
        protected readonly IUsuarioRepositorio _repositorio;
        private readonly ICursoRepositorio _cursoRepositorio;

        public UsuarioComandoValidador(IUsuarioRepositorio repositorio, ICursoRepositorio cursoRepositorio)
        {
            _repositorio = repositorio;
            _cursoRepositorio = cursoRepositorio;

            RuleFor(lnq => lnq.Email).NotEmpty().WithMessage("O campo de e-mail não pode ser vazio.");
            RuleFor(lnq => lnq.Login).NotEmpty().WithMessage("O campo de login não pode ser vazio.");
            RuleFor(lnq => lnq.Nome).NotEmpty().WithMessage("O campo de nome não pode ser vazio.");
            RuleFor(lnq => lnq.PerfilCodigo).NotEmpty().WithMessage("O campo de perfil não pode ser vazio.");
         
            RuleFor(lnq => lnq)
                .MustAsync(ValidarUsuarioComMesmoEmail)
                .WithMessage(comando => $"Já existe um usuário cadastrado com o e-mail {comando.Email}.")
                .MustAsync(ValidarUsuarioComMesmoLogin)
                .WithMessage(comando => $"Já existe um usuário cadastrado com o login {comando.Login}.");

            When(lnq => lnq.PerfilCodigo > 0, () => {
                RuleFor(lnq => lnq)
                   .Must(ValidarSeUsuarioNaoPossuiCursoVinculado)
                   .WithMessage("Somente usuários com perfil coordenador de curso podem ter cursos vinculados.")
                   .MustAsync(ValidarSeUsuarioCoordenadorPossuiCursoVinculado)
                   .WithMessage("O usuário coordenador precisa ter um curso vinculado ao perfil.");
            });
        }

        private async Task<bool> ValidarSeUsuarioCoordenadorPossuiCursoVinculado(T comando, CancellationToken arg2)
        {
            if (comando.PerfilCodigo == 3 && comando.CursoCodigo <= 0)
                return false;

            var existeCurso = await _cursoRepositorio.Contem(lnq => lnq.Codigo == comando.CursoCodigo);

            return existeCurso;
        }

        private bool ValidarSeUsuarioNaoPossuiCursoVinculado(T comando)
        {
            if (comando.PerfilCodigo != 3 && comando.CursoCodigo > 0)
                return false;

            return true;
        }

        protected async Task<bool> ValidarUsuarioComMesmoEmail(T comando, CancellationToken cancellationToken)
        {
            var msmEmail = await _repositorio
                            .Contem(lnq => lnq.Email.IgualA(comando.Email));

            if (comando.Codigo.HasValue)
                msmEmail = await _repositorio
                                .Contem(lnq => lnq.Email.IgualA(comando.Email)
                                 && lnq.Codigo != comando.Codigo);

            if (msmEmail)
                return false;

            return true;
        }

        protected async Task<bool> ValidarUsuarioComMesmoLogin(T comando, CancellationToken cancellationToken)
        {
            var msmLogin = await _repositorio.Contem(lnq => lnq.Login.IgualA(comando.Login)
                                 && comando.Codigo != lnq.Codigo);

            if (msmLogin)
                return false;

            return true;
        }

    }
}
