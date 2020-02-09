using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComandoValidador : AbstractValidator<CriarCargoComando>, ICriarCargoComandoValidador
    {
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly ICurriculoRepositorio _curriculoReposiotorio;
        private readonly ICargoRepositorio _cargoRepositorio;

        public CriarCargoComandoValidador(IProfessorRepositorio professorRepositorio, ICurriculoRepositorio curriculoRepositorio, ICargoRepositorio cargoRepositorio)
        {
            _professorRepositorio = professorRepositorio;
            _curriculoReposiotorio = curriculoRepositorio;
            _cargoRepositorio = cargoRepositorio;

            RuleFor(lnq => lnq.Ano).NotEmpty().WithMessage("O campo ano é obrigatório.");
            RuleFor(lnq => lnq.Edital).NotEmpty().WithMessage("O campo edital é obrigatório.");
            RuleFor(lnq => lnq.Numero).NotEmpty().WithMessage("O campo número é obrigatório.");
            RuleFor(lnq => lnq.Semestre).NotEmpty().WithMessage("O campo semestre é obrigatório.");
            RuleFor(lnq => lnq.Disciplinas).NotEmpty().WithMessage("Para realizar o cadastro é necessário informar pelo menos uma disciplina.");
            RuleFor(lnq => lnq).MustAsync(ValidarSeCargoJaCadastrado).WithMessage("Já existe um cargo com os mesmos valores para os campos semestre, ano, edital e número.");

            When(lnq => lnq.CodigoProfessor.HasValue, () => {
                RuleFor(lnq => lnq.CodigoProfessor).MustAsync(ValidarSeProfessorExiste).WithMessage(c => $"Não foi encontrado um professor com o código {c.CodigoProfessor}");
            });

            When(lnq => lnq.Disciplinas != null && lnq.Disciplinas.Count() > 0, () =>
            {
                RuleFor(lnq => lnq.Disciplinas).MustAsync(ValidarSeCurriculoDisciplinaExiste).WithMessage("Currículo não encontrado para alguma disciplina informada.");
            });
        }
        
        private async Task<bool> ValidarSeProfessorExiste(int? codigoProfessor, CancellationToken cancellationToken)
        {
           var resultado = await _professorRepositorio.Contem(lnq => lnq.Codigo == codigoProfessor.Value && lnq.Ativo == true);
           return resultado;
        }

        private async Task<bool> ValidarSeCurriculoDisciplinaExiste(IEnumerable<CargoDisciplinaViewModel> disciplinas, CancellationToken cancellationToken)
        {
            foreach (var disciplina in disciplinas)
            {
                var curriculoDisciplina = await _curriculoReposiotorio.ConsultarCurriculoDisciplina(disciplina.CodigoCurriculoDisciplina.Value);
                if (curriculoDisciplina == null)
                    return false;
            }

            return true;

        }

        private async Task<bool> ValidarSeCargoJaCadastrado(CriarCargoComando comando, CancellationToken arg2)
        {
            var resultado = await _cargoRepositorio.Contem(lnq => lnq.Semestre == comando.Semestre && 
                                                           lnq.Ano == comando.Ano && 
                                                           lnq.Edital == comando.Edital && 
                                                           lnq.Numero == comando.Numero);
            return !resultado;
        }
    }
}
