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

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Atualizar
{
    public class AtualizarHorarioAulaComandoHandler : IRequestHandler<AtualizarHorarioAulaComando, Resposta<HorarioAulaViewModel>>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;
        private readonly IValidador<AtualizarHorarioAulaComando> _validador;
        private readonly IMapper _mapper;

        public AtualizarHorarioAulaComandoHandler(IHorarioAulaRepositorio horarioAulaRepositorio,
                                                  IValidador<AtualizarHorarioAulaComando> validador,
                                                  IMapper mapper)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<HorarioAulaViewModel>> Handle(AtualizarHorarioAulaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<HorarioAulaViewModel>(erro);

            var horarioModel = _mapper.Map<HorarioAula>(request);
          
            horarioModel = await _horarioAulaRepositorio.Atualizar(horarioModel);
          
            var horarioViewModel = _mapper.Map<HorarioAulaViewModel>(horarioModel);
          
            return new Resposta<HorarioAulaViewModel>(horarioViewModel);
        }
    }
}
