using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.ViewModel;
using AutoMapper;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Consultas.ListarDisciplinas
{
    public class ListarDisciplinasCurriculoConsultaHandler : IRequestHandler<ListarDisciplinasCurriculoConsulta, Resposta<List<CurriculoDisciplinaViewModel>>>
    {
        private readonly ICurriculoDisciplinaRepositorio _repositorio;
        private readonly IValidador<ListarDisciplinasCurriculoConsulta> _validador;
        private readonly IMapper _mapper;

        public ListarDisciplinasCurriculoConsultaHandler(ICurriculoDisciplinaRepositorio repositorio,
                                                         IValidador<ListarDisciplinasCurriculoConsulta> validador,
                                                         IMapper mapper)
        {
            _repositorio = repositorio;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<List<CurriculoDisciplinaViewModel>>> Handle(ListarDisciplinasCurriculoConsulta request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<List<CurriculoDisciplinaViewModel>>(erros);

            var resultado = await _repositorio.Listar(lnq => lnq.CodigoCurriculo == request.CodigoCurriculo);

            var disciplinasViewModel = _mapper.Map<List<CurriculoDisciplinaViewModel>>(resultado);

            return new Resposta<List<CurriculoDisciplinaViewModel>>(disciplinasViewModel);
        }
    }
}
