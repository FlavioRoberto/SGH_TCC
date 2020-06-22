using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cargos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoCargoConsultaHandler : IRequestHandler<ListarPaginacaoCargoConsulta, Paginacao<CargoViewModel>>
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly IMapper _mapper;

        public ListarPaginacaoCargoConsultaHandler(ICargoRepositorio cargoRepositorio, IMapper mapper)
        {
            _cargoRepositorio = cargoRepositorio;
            _mapper = mapper;
        }

        public async Task<Paginacao<CargoViewModel>> Handle(ListarPaginacaoCargoConsulta request, CancellationToken cancellationToken)
        {
            var cargoPaginadoViewModel = _mapper.Map<Paginacao<Cargo>>(request.CargoPaginado);

            var resultado = await _cargoRepositorio.ListarPorPaginacao(cargoPaginadoViewModel);

            var entidadeMapeada = _mapper.Map<Paginacao<CargoViewModel>>(resultado);

            return entidadeMapeada;
        }
    }
}
