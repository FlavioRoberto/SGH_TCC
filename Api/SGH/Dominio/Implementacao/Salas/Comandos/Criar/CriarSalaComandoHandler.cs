using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar
{
    public class CriarSalaComandoHandler : IRequestHandler<CriarSalaComando, Resposta<SalaViewModel>>
    {
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly IMapper _mapper;

        public CriarSalaComandoHandler(ISalaRepositorio salaRepositorio, IMapper mapper)
        {
            _salaRepositorio = salaRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<SalaViewModel>> Handle(CriarSalaComando request, CancellationToken cancellationToken)
        {
            var salaEntidade = _mapper.Map<Sala>(request);
            
            salaEntidade = await _salaRepositorio.Criar(salaEntidade);

            var salaViewModel = _mapper.Map<SalaViewModel>(salaEntidade);

            return new Resposta<SalaViewModel>(salaViewModel);
        }
    }
}
