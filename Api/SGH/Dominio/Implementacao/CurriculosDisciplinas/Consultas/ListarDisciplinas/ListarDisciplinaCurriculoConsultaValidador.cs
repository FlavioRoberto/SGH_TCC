using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Consultas.ListarDisciplinas
{
    public class ListarDisciplinaCurriculoConsultaValidador : AbstractValidator<ListarDisciplinasCurriculoConsulta>, IListarDisciplinaCurriculoConsultaValidador
    {
        private readonly ICurriculoRepositorio _repositorio;

        public ListarDisciplinaCurriculoConsultaValidador(ICurriculoRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.CodigoCurriculo).NotEmpty().WithMessage("O código do currículo não foi informado.");
        }
    }
}
