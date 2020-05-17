using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarTodas
{
    public class ListarTodasDisciplinasCargoConsultaHandler : IRequestHandler<ListarTodasDisciplinasCargoConsulta, Resposta<List<CargoDisciplinaViewModel>>>
    {
        private readonly ICargoDisciplinaRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidador<ListarTodasDisciplinasCargoConsulta> _validador;

        public ListarTodasDisciplinasCargoConsultaHandler(ICargoDisciplinaRepositorio repositorio, IMapper mapper, IValidador<ListarTodasDisciplinasCargoConsulta> validador)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _validador = validador;
        }

        public async Task<Resposta<List<CargoDisciplinaViewModel>>> Handle(ListarTodasDisciplinasCargoConsulta request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<List<CargoDisciplinaViewModel>>(erro);

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
                    CursoDescricao = $"{curriculoDisciplina.Curso.Descricao} - {curriculoDisciplina.Ano}",
                    TurnoDescricao = cargoDisciplina.Turno.Descricao,
                    CodigoTurno = cargoDisciplina.CodigoTurno,
                    Descricao = cargoDisciplina.Descricao ?? disciplina.Descricao,
                    CodigoCurriculo = curriculoDisciplina.Codigo
                };

                disciplinasCurriculo.Add(cargoDisciplinaViewModel);
            }

            return new Resposta<List<CargoDisciplinaViewModel>>(disciplinasCurriculo);

        }
    }
}
