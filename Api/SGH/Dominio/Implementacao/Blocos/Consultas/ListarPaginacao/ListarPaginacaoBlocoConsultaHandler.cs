using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoBlocoConsultaHandler : IRequestHandler<ListarPaginacaoBlocoConsulta, Paginacao<BlocoViewModel>>
    {
        private readonly IBlocoRepositorio _blocoRepositorio;
        private readonly IMapper _mapper;

        public ListarPaginacaoBlocoConsultaHandler(IBlocoRepositorio blocoRepositorio, IMapper mapper)
        {
            _blocoRepositorio = blocoRepositorio;
            _mapper = mapper;
        }

        public async Task<Paginacao<BlocoViewModel>> Handle(ListarPaginacaoBlocoConsulta request, CancellationToken cancellationToken)
        {
            var blocoPaginadoViewModel = _mapper.Map<Paginacao<Bloco>>(request.BlocoPaginado);

            var resultado = await _blocoRepositorio.ListarPorPaginacao(blocoPaginadoViewModel);

            var entidadeMapeada = _mapper.Map<Paginacao<BlocoViewModel>>(resultado);

            return entidadeMapeada;
        }
    }
}
