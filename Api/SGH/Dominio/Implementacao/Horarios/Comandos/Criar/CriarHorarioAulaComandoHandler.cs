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

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar
{
    public class CriarHorarioAulaComandoHandler : IRequestHandler<CriarHorarioAulaComando, Resposta<HorarioAulaViewModel>>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;
        private readonly IValidador<CriarHorarioAulaComando> _validador;
        private readonly IMapper _mapper;

        public CriarHorarioAulaComandoHandler(IHorarioAulaRepositorio horarioAulaRepositorio,
                                              IValidador<CriarHorarioAulaComando> validador, 
                                              IMapper mapper)
        {
            _mapper = mapper;
            _validador = validador;
            _horarioAulaRepositorio = horarioAulaRepositorio;
        }

        public async Task<Resposta<HorarioAulaViewModel>> Handle(CriarHorarioAulaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<HorarioAulaViewModel>(erro);

            var horarioInserir = _mapper.Map<Horario>(request);

            var horario = await _horarioAulaRepositorio.Criar(horarioInserir);

            var horarioViewModel = _mapper.Map<HorarioAulaViewModel>(horario);

            return new Resposta<HorarioAulaViewModel>(horarioViewModel);
        }
    }
}
