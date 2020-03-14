using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Criar
{
    public class CriarCurriculoComandoValidador : AbstractValidator<CriarCurriculoComando>, IValidador<CriarCurriculoComando>
    {
        private readonly ICurriculoRepositorio _repositorio;

        public CriarCurriculoComandoValidador(ICurriculoRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.CodigoCurso).NotEmpty().WithMessage("Código do curso não foi informado.");
            RuleFor(lnq => lnq.Ano).NotEmpty().WithMessage("Ano não foi informado.");
            RuleFor(lnq => lnq).MustAsync(ValidarCurriculoExistent).WithMessage("Já existe um currículo cadastrado com os dados informados!");
        }

        private async Task<bool> ValidarCurriculoExistent(CriarCurriculoComando comando, CancellationToken arg2)
        {
            var curriculoExistente = await _repositorio.Contem(lnq => lnq.Ano == comando.Ano
                                                               && lnq.CodigoCurso == comando.CodigoCurso);
            return !curriculoExistente;
        }
    }
}
