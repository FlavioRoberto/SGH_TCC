using FluentValidation;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar
{
    public class CriarCurriculoDisciplinaComandoValidador : CurriculoDisciplinaComandoBaseValidador<CriarCurriculoDisciplinaComando>, IValidador<CriarCurriculoDisciplinaComando>
    {
        public CriarCurriculoDisciplinaComandoValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                        IDisciplinaRepositorio disciplinaRepositorio,
                                                        ICurriculoRepositorio curriculoRepositorio) :
                                                        base(curriculoDisciplinaRepositorio, disciplinaRepositorio, curriculoRepositorio)
        {
            RuleFor(lnq => lnq).MustAsync(ValidarSeDisciplinaCurriculoJaAdicionada).WithMessage(c => $"Disciplina já adicionada neste currículo.");
        }

        private async Task<bool> ValidarSeDisciplinaCurriculoJaAdicionada(CriarCurriculoDisciplinaComando comando, CancellationToken arg2)
        {
            var resultado = await _curriculoDisciplinaRepositorio.Contem(lnq => lnq.CodigoDisciplina == comando.CodigoDisciplina && lnq.CodigoCurriculo == comando.CodigoCurriculo);
            return !resultado;
        }
    }
}
