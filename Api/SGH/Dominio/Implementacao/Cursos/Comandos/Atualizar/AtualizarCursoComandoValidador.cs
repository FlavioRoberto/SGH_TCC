using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Shared.Extensions;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Atualizar
{
    class AtualizarCursoComandoValidador : CursoComandoValidador<AtualizarCursoComando>
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public AtualizarCursoComandoValidador(ICursoRepositorio cursoRepositorio)
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            _cursoRepositorio = cursoRepositorio;

            RuleFor(lnq => lnq.Codigo)
                .NotEmpty()
                .WithMessage("Código não pode ser vazio.")
                .GreaterThan(0)
                .WithMessage("O código deve ser maior que 0.");

            When(lnq => lnq.Codigo.HasValue, () =>
            {
                RuleFor(lnq => lnq)
                    .MustAsync(ValidarSeDescricaoExiste)
                    .WithMessage("Não foi possível editar o curso, pois já existe um curso com a descrição informada.");
            }); 
        }

        private async Task<bool> ValidarSeDescricaoExiste(AtualizarCursoComando comando, CancellationToken arg2)
        {
            var existeDescricao = await _cursoRepositorio.Contem(lnq => lnq.Descricao.IgualA(comando.Descricao) &&
                                                                        lnq.Codigo != comando.Codigo);

            if (existeDescricao)
                return false;

            return true;
        }
    }
}
