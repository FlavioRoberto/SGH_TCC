using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComandoHandler : IRequestHandler<CriarCargoComando,Resposta<CriarCargoComando>>
    {
        private readonly ICargoRepositorio _repositorioCargo;

        public CriarCargoComandoHandler(ICargoRepositorio repositorio)
        {
            _repositorioCargo = repositorio;
        }

        public async Task<Resposta<CriarCargoComando>> Handle(CriarCargoComando request, CancellationToken cancellationToken)
        {
            var entidade = new Cargo
            {
                Ano = request.Ano,
                CodigoProfessor = request.CodigoProfessor,
                Edital = request.Edital,
                Numero = request.Numero,
                Semestre = request.Semestre
            };

            var resultado = await _repositorioCargo.Criar(entidade);

            request.Codigo = resultado.Codigo;

            request.Disciplinas = await CriarDisciplinasCargo(request.Disciplinas, entidade.Codigo);

            return new Resposta<CriarCargoComando>(request);
        }

        private async Task<List<CargoDisciplinaViewModel>> CriarDisciplinasCargo(IEnumerable<CargoDisciplinaViewModel> disciplinas, int codigoCargo)
        {
            var disciplinasAdicionadas = new List<CargoDisciplinaViewModel>();

            foreach(var disciplina in disciplinas)
            {
                disciplina.CodigoCargo = codigoCargo;

                var entidade = new CargoDisciplina
                {
                   CodigoCargo = disciplina.CodigoCargo.Value,
                   CodigoCurriculoDisciplina = disciplina.CodigoCurriculoDisciplina.Value
                };

                await _repositorioCargo.CriarDisciplina(entidade);

                disciplina.Codigo = entidade.Codigo;

                disciplinasAdicionadas.Add(disciplina);
            }

            return disciplinasAdicionadas;
        }
    }
}
