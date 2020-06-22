using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Services.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar
{
    public class CriarBlocoComandoHandler : IRequestHandler<CriarBlocoComando, Resposta<BlocoViewModel>>
    {
        private readonly IBlocoRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidador<CriarBlocoComando> _validador;

        public CriarBlocoComandoHandler(IBlocoRepositorio repositorio, IMapper mapper, IValidador<CriarBlocoComando> validador)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _validador = validador;
        }

        public async Task<Resposta<BlocoViewModel>> Handle(CriarBlocoComando request, CancellationToken cancellationToken)
        {
            var mensagemErro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(mensagemErro))
                return new Resposta<BlocoViewModel>(mensagemErro);

            var blocoEntidade = _mapper.Map<Bloco>(request);
            
            blocoEntidade = await _repositorio.Criar(blocoEntidade);
          
            var blocoViewModel = _mapper.Map<BlocoViewModel>(blocoEntidade);
           
            return new Resposta<BlocoViewModel>(blocoViewModel);
        }
    }
}
