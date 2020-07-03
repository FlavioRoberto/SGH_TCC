using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoConsultaHandler : IRequestHandler<ListarPaginacaoTurnoConsulta, Paginacao<TurnoViewModel>>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ListarPaginacaoConsultaHandler(ITurnoRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Paginacao<TurnoViewModel>> Handle(ListarPaginacaoTurnoConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarPorPaginacao(request.TurnoPaginado);
            return _mapper.Map<Paginacao<TurnoViewModel>>(resultado);
        }
    }
}
