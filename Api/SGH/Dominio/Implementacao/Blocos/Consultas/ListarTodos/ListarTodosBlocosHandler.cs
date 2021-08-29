using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Consultas.ListarTodos
{
    public class ListarTodosBlocosHandler : IRequestHandler<ListarTodosBlocoConsulta, List<BlocoViewModel>>
    {
        private readonly IBlocoRepositorio _blocoRepositorio;
        private readonly IMapper _mapper;

        public ListarTodosBlocosHandler(IBlocoRepositorio blocoRepositorio, IMapper mapper)
        {
            _blocoRepositorio = blocoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<BlocoViewModel>> Handle(ListarTodosBlocoConsulta request, CancellationToken cancellationToken)
        {
            var blocos = await _blocoRepositorio.ListarTodos();
            return _mapper.Map<List<BlocoViewModel>>(blocos);
        }
    }
}
