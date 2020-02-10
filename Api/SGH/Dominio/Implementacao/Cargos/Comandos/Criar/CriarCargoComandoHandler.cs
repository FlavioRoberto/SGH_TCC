using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComandoHandler : IRequestHandler<CriarCargoComando,Resposta<CargoViewModel>>
    {
        private readonly ICargoRepositorio _repositorioCargo;
        private readonly ICargoDisciplinaRepositorio _repositorioCargoDisciplina;
        private readonly ICriarCargoComandoValidador _validador;
        private readonly IMapper _mapper;

        public CriarCargoComandoHandler(ICargoRepositorio repositorio, 
                                        ICargoDisciplinaRepositorio repositorioCargoDisciplina, 
                                        ICriarCargoComandoValidador validador,
                                        IMapper mapper)
        {
            _repositorioCargo = repositorio;
            _repositorioCargoDisciplina = repositorioCargoDisciplina;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<CargoViewModel>> Handle(CriarCargoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CargoViewModel>(erros);

            var entidade = new Cargo
            {
                Ano = request.Ano,
                CodigoProfessor = request.CodigoProfessor,
                Edital = request.Edital,
                Numero = request.Numero,
                Semestre = request.Semestre
            };

            var resultado = await _repositorioCargo.Criar(entidade);

            var viewModel = _mapper.Map<CargoViewModel>(resultado);

            viewModel.Disciplinas = await CriarDisciplinasCargo(request.Disciplinas, entidade.Codigo);

            return new Resposta<CargoViewModel>(viewModel);
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

                await _repositorioCargoDisciplina.Criar(entidade);

                disciplina.Codigo = entidade.Codigo;

                disciplinasAdicionadas.Add(disciplina);
            }

            return disciplinasAdicionadas;
        }
    }
}
