using FluentValidation;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos
{
    public abstract class ProfessorComandoValidador<T> : Validador<T> where T : IProfessorComando
    {
        protected readonly IProfessorRepositorio _repositorio;

        public ProfessorComandoValidador(IProfessorRepositorio repositorio)
        {
            _repositorio = repositorio;
            RuleFor(lnq => lnq.Email).NotEmpty().WithMessage("O campo e-mail não pode ser vazio..");
            RuleFor(lnq => lnq.Matricula).NotEmpty().WithMessage("O campo matrícula não pode ser vazio.");
            RuleFor(lnq => lnq.Nome).NotEmpty().WithMessage("O campo nome não pode ser vazio.");
            RuleFor(lnq => lnq.Contratacao).NotEmpty().WithMessage("O campo contratação não pode ser vazio.");
        }
    }
}
