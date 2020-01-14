using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Professores.Comandos
{
    public abstract class ProfessorComandoValidador<T> : AbstractValidator<T> where T : IProfessorComando
    {
        protected readonly IProfessorRepositorio _repositorio;

        public ProfessorComandoValidador(IProfessorRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.Email).NotEmpty().WithMessage("O campo e-mail não pode ser vazio..");
            RuleFor(lnq => lnq.Matricula).NotEmpty().WithMessage("O campo matrícula não pode ser vazio.");
            RuleFor(lnq => lnq.Nome).NotEmpty().WithMessage("O campo nome não pode ser vazio.");
            RuleFor(lnq => lnq.Telefone).NotEmpty().WithMessage("O campo telefone não pode ser vazio.");
        }
    }
}
