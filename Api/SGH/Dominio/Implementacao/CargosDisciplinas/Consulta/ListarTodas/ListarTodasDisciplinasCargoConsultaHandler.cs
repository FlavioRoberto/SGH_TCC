using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.CargosDisciplinas.Consulta.ListarTodas
{
    public class ListarTodasDisciplinasCargoConsultaHandler : IRequestHandler<ListarTodasDisciplinasCargoConsulta, List<CargoDisciplinaViewModel>>
    {
        private readonly ICargoDisciplinaRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ListarTodasDisciplinasCargoConsultaHandler(ICargoDisciplinaRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<List<CargoDisciplinaViewModel>> Handle(ListarTodasDisciplinasCargoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Listar(lnq => lnq.CodigoCargo == request.CodigoCargo);

            var disciplinasCurriculo = new List<CargoDisciplinaViewModel>();

            foreach(var cargoDisciplina in resultado)
            {
                var disciplina = await _repositorio.RetornarDisciplina(cargoDisciplina.CodigoCurriculoDisciplina);

                var curriculoDisciplina = await _repositorio.RetornarCurriculoDisciplina(cargoDisciplina.CodigoCurriculoDisciplina);

                var cargoDisciplinaViewModel = new CargoDisciplinaViewModel
                {
                    Codigo = cargoDisciplina.Codigo,
                    CodigoCargo = cargoDisciplina.CodigoCargo,
                    CodigoCurriculoDisciplina = cargoDisciplina.CodigoCurriculoDisciplina,
                    cursoDescricao = $"{curriculoDisciplina.Curso.Descricao} - {curriculoDisciplina.Ano}",
                    disciplinaDescricao = disciplina.Descricao
                };

                disciplinasCurriculo.Add(cargoDisciplinaViewModel);
            }

            return disciplinasCurriculo;

        }
    }
}
