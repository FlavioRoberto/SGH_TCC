using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarDisciplinas
{
    public class ListarDisciplinaCurriculoConsultaValidador : AbstractValidator<ListarDisciplinasCurriculoConsulta>, IListarDisciplinaCurriculoConsultaValidador
    {
        private readonly ICurriculoRepositorio _repositorio;

        public ListarDisciplinaCurriculoConsultaValidador(ICurriculoRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(lnq => lnq.CodigoCurriculo).NotEmpty().WithMessage("O código do currículo não foi informado.");
            RuleFor(lnq => lnq.CodigoCurriculo).MustAsync(ValidarDisciplinasAdicionadas).WithMessage(consulta => $"Não foram vinculadas disciplinas para o currículo {consulta.CodigoCurriculo}");
        }

        private async Task<bool> ValidarDisciplinasAdicionadas(int codigoCurriculo, CancellationToken cancellationToken)
        {
            int quantidadeDisciplina = await _repositorio.RetornarQuantidadeDisciplinaCurriculo(codigoCurriculo);

            if (quantidadeDisciplina <= 0)
                return false;

            return true;
        }
    }
}
