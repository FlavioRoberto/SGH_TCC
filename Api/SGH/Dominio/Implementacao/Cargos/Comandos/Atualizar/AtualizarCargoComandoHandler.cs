using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Atualizar
{
    public class AtualizarCargoComandoHandler : IRequestHandler<AtualizarCargoComando, Resposta<CargoViewModel>>
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IMapper _mapper;

        public AtualizarCargoComandoHandler(ICargoRepositorio cargoRepositorio, ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, IMapper mapper)
        {
            _cargoRepositorio = cargoRepositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<CargoViewModel>> Handle(AtualizarCargoComando request, CancellationToken cancellationToken)
        {
            var cargo = _mapper.Map<Cargo>(request);
            var disciplinas = await AtualizarDisciplinas(request);
            cargo.Disciplinas = null;
            var cargoAtualizado = await _cargoRepositorio.Atualizar(cargo);
            cargoAtualizado.Disciplinas = disciplinas;
            var cargoViewModel = _mapper.Map<CargoViewModel>(cargoAtualizado);
            return new Resposta<CargoViewModel>(cargoViewModel);
        }

        private async Task<IEnumerable<CargoDisciplina>> AtualizarDisciplinas(AtualizarCargoComando comando)
        {
            var disciplinasCargo = await _cargoDisciplinaRepositorio.Listar(lnq => lnq.CodigoCargo == comando.Codigo);

            var disciplinasCargoAtualizadas = new List<CargoDisciplina>();

            foreach (var disciplina in comando.Disciplinas)
            {
                var disciplinaEstaNaLista = disciplinasCargo.Any(lnq => lnq.CodigoCargo == disciplina.CodigoCargo && lnq.CodigoCurriculoDisciplina == disciplina.CodigoCurriculoDisciplina);

                bool adicionarDisciplina = !disciplinaEstaNaLista && !disciplina.Codigo.HasValue;

                var disciplinaEntidade = _mapper.Map<CargoDisciplina>(disciplina);

                if (adicionarDisciplina)
                {
                    var disciplinaCriada = await _cargoDisciplinaRepositorio.Criar(disciplinaEntidade);
                    disciplinasCargoAtualizadas.Add(disciplinaCriada);
                    continue;
                }
                
                var atualizarDisciplina = disciplinasCargo.Any(lnq => lnq.Codigo == disciplina.Codigo && lnq.CodigoCurriculoDisciplina != disciplina.CodigoCurriculoDisciplina);

                if (atualizarDisciplina)
                {
                   var disciplinaAtualizada = await _cargoDisciplinaRepositorio.Atualizar(disciplinaEntidade);
                   disciplinasCargoAtualizadas.Add(disciplinaAtualizada);
                   continue;
                }

                disciplinasCargoAtualizadas.Add(disciplinaEntidade);
            }

            foreach (var disciplinasARemover in disciplinasCargo)
            {
                var removerDisciplina = !comando.Disciplinas.Any(lnq => lnq.Codigo == disciplinasARemover.Codigo);

                if (removerDisciplina)
                    await _cargoDisciplinaRepositorio.Remover(lnq => lnq.Codigo == disciplinasARemover.Codigo);
            }

            return disciplinasCargoAtualizadas;
        }

    }
}
