using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar
{
    public class CriarBlocoComandoHandler : IRequestHandler<CriarBlocoComando, Resposta<BlocoViewModel>>
    {
        private readonly IBlocoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public CriarBlocoComandoHandler(IBlocoRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<BlocoViewModel>> Handle(CriarBlocoComando request, CancellationToken cancellationToken)
        {
            var blocoEntidade = _mapper.Map<Bloco>(request);
            
            blocoEntidade = await _repositorio.Criar(blocoEntidade);
          
            var blocoViewModel = _mapper.Map<BlocoViewModel>(blocoEntidade);
           
            return new Resposta<BlocoViewModel>(blocoViewModel);
        }
    }
}
