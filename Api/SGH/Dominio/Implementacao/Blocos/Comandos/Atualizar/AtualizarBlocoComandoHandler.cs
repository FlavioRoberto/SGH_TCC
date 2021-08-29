using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Services.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Atualizar
{
    public class AtualizarBlocoComandoHandler : IRequestHandler<AtualizarBlocoComando, Resposta<BlocoViewModel>>
    {
        private readonly IBlocoRepositorio _blocoRepositorio;
        private readonly IMapper _mapper;
        private readonly IValidador<AtualizarBlocoComando> _validador;

        public AtualizarBlocoComandoHandler(IBlocoRepositorio blocoRepositorio, IMapper mapper, IValidador<AtualizarBlocoComando> validador)
        {
            _blocoRepositorio = blocoRepositorio;
            _mapper = mapper;
            _validador = validador;
        }

        public async Task<Resposta<BlocoViewModel>> Handle(AtualizarBlocoComando request, CancellationToken cancellationToken)
        {
            var mensagemErro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(mensagemErro))
                return new Resposta<BlocoViewModel>(mensagemErro);

            var blocoEntidade = _mapper.Map<Bloco>(request);

            blocoEntidade = await _blocoRepositorio.Atualizar(blocoEntidade);

            var blocoViewModel = _mapper.Map<BlocoViewModel>(blocoEntidade);

            return new Resposta<BlocoViewModel>(blocoViewModel);
        }
    }
}
