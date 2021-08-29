using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar
{
    public class CriarTurnoComandoHandler : IRequestHandler<CriarTurnoComando, Resposta<TurnoViewModel>>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly IValidador<CriarTurnoComando> _validador;
        private readonly IMapper _mapper;

        public CriarTurnoComandoHandler(ITurnoRepositorio repositorio, IValidador<CriarTurnoComando> validador, IMapper mapper)
        {
            _repositorio = repositorio;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<TurnoViewModel>> Handle(CriarTurnoComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<TurnoViewModel>(erro);

            var turno = new Turno
            {
                Descricao = request.Descricao,
                Horarios = string.Join(", ", request.Horarios)
            };

            var resultado = await _repositorio.Criar(turno);

            var turnoViewModel = _mapper.Map<TurnoViewModel>(resultado);

            return new Resposta<TurnoViewModel>(turnoViewModel);
        }
    }
}
