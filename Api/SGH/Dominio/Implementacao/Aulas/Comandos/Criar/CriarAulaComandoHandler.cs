using AutoMapper;
using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar
{
    public class CriarAulaComandoHandler : IRequestHandler<CriarAulaComando, Resposta<AulaViewModel>>
    {
        private readonly IValidador<CriarAulaComando> _validador;
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly IMapper _mapper;

        public CriarAulaComandoHandler(IValidador<CriarAulaComando> validador, IAulaRepositorio aulaRepositorio, IMapper mapper)
        {
            _validador = validador;
            _mapper = mapper;
            _aulaRepositorio = aulaRepositorio;
        }

        public async Task<Resposta<AulaViewModel>> Handle(CriarAulaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<AulaViewModel>(erros);

            var aula = _mapper.Map<Aula>(request);

            aula = await SalvarAula(aula);

            var aulaViewModel = _mapper.Map<AulaViewModel>(aula);

            return new Resposta<AulaViewModel>(aulaViewModel);
        }

        private async Task<Aula> SalvarAula(Aula aula)
        {
            return await _aulaRepositorio.Criar(aula);
        }
    }
}
