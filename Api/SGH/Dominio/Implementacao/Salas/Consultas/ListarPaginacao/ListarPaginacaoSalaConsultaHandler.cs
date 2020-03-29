using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Consultas.ListarPaginacao
{
    public class ListarPaginacaoSalaConsultaHandler : IRequestHandler<ListarPaginacaoSalaConsulta, Paginacao<Sala>>
    {
        private readonly ISalaRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ListarPaginacaoSalaConsultaHandler(ISalaRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Paginacao<Sala>> Handle(ListarPaginacaoSalaConsulta request, CancellationToken cancellationToken)
        {
            var salaPaginada = _mapper.Map<Paginacao<Sala>>(request.SalaPaginado);
            return await _repositorio.ListarPorPaginacao(salaPaginada);
        }
    }
}
