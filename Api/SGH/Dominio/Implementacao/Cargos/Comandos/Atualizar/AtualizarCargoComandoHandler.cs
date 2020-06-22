using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar
{
    public class AtualizarCargoComandoHandler : IRequestHandler<AtualizarCargoComando, Resposta<CargoViewModel>>
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly IMapper _mapper;
        private readonly IValidador<AtualizarCargoComando> _validador;

        public AtualizarCargoComandoHandler(ICargoRepositorio cargoRepositorio, IMapper mapper, IValidador<AtualizarCargoComando> validador)
        {
            _cargoRepositorio = cargoRepositorio;
            _mapper = mapper;
            _validador = validador;
        }

        public async Task<Resposta<CargoViewModel>> Handle(AtualizarCargoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);
            
            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CargoViewModel>(erros);

            var cargo = _mapper.Map<Cargo>(request);
            
            var cargoAtualizado = await _cargoRepositorio.Atualizar(cargo);
            
            var cargoViewModel = _mapper.Map<CargoViewModel>(cargoAtualizado);
            
            return new Resposta<CargoViewModel>(cargoViewModel);
        }       
    }
}
