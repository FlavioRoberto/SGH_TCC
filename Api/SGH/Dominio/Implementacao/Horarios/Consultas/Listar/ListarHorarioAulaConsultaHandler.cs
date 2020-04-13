using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Horarios.Consultas.Listar
{
    public class ListarHorarioAulaConsultaHandler : IRequestHandler<ListarHorarioAulaConsulta, List<HorarioAulaViewModel>>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;
        private readonly IMapper _mapper;

        public ListarHorarioAulaConsultaHandler(IHorarioAulaRepositorio horarioAulaRepositorio, IMapper mapper)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;
            _mapper = mapper; 
        }

        public async Task<List<HorarioAulaViewModel>> Handle(ListarHorarioAulaConsulta request, CancellationToken cancellationToken)
        {
            List<HorarioAula> horarios = await ListarHorarios(request);

            var horariosViewModel = _mapper.Map<List<HorarioAulaViewModel>>(horarios);

            return horariosViewModel;
        }

        private async Task<List<HorarioAula>> ListarHorarios(ListarHorarioAulaConsulta request)
        {
            return await _horarioAulaRepositorio.Listar(new ListarHorarioFiltro
            {
                Ano = request.Ano,
                CodigoCurriculo = request.CodigoCurriculo,
                Periodo = request.Periodo,
                Semestre = request.Semestre
            });
        }
    }
}
