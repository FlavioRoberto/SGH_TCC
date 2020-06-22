using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.ViewModel;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo
{
    public class ListarDisciplinasCargoPorCurriculoConsultaHandler : IRequestHandler<ListarDisciplinaCargoPorCurriculoConsulta, Resposta<ICollection<CargoDisciplinaListarPorCurriculoViewModel>>>
    {
        private readonly IValidador<ListarDisciplinaCargoPorCurriculoConsulta> _validador;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        private readonly IMapper _mapper;

        public ListarDisciplinasCargoPorCurriculoConsultaHandler(IValidador<ListarDisciplinaCargoPorCurriculoConsulta> validador,
                                                                 ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                                 ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                                 IMapper mapper)
        {
            _validador = validador;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<ICollection<CargoDisciplinaListarPorCurriculoViewModel>>> Handle(ListarDisciplinaCargoPorCurriculoConsulta request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<ICollection<CargoDisciplinaListarPorCurriculoViewModel>>(erro);

            var disciplinasCurriculo = await _curriculoDisciplinaRepositorio.Listar(lnq => lnq.CodigoCurriculo == request.CodigoCurriculo &&
                                                                                           lnq.Periodo == request.Periodo);

            var codigoDisciplinasCurriculo = disciplinasCurriculo.Select(lnq => lnq.Codigo).ToList();

            var disciplinasCargo = await _cargoDisciplinaRepositorio.ListarDisciplinasCurriculo(lnq => lnq.Cargo.Ano == request.Ano &&
                                                                                                      lnq.Cargo.Semestre == request.Semestre &&
                                                                                                      lnq.Turno.Codigo == request.CodigoTurno &&
                                                                                                      codigoDisciplinasCurriculo.Contains(lnq.CodigoCurriculoDisciplina));

            var disciplinas = _mapper.Map<List<CargoDisciplinaListarPorCurriculoViewModel>>(disciplinasCargo);

            return new Resposta<ICollection<CargoDisciplinaListarPorCurriculoViewModel>>(disciplinas);
        }
    }
}
