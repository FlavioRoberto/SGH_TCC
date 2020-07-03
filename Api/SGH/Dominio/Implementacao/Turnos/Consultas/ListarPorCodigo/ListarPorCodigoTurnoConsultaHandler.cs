using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPorCodigo
{
    public class ListarPorCodigoTurnoConsultaHandler : IRequestHandler<ListarPorCodigoTurnoConsulta, TurnoViewModel>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ListarPorCodigoTurnoConsultaHandler(ITurnoRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<TurnoViewModel> Handle(ListarPorCodigoTurnoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Consultar(lnq => lnq.Codigo == request.TurnoId);
            return _mapper.Map<TurnoViewModel>(resultado);
        }
    }
}
