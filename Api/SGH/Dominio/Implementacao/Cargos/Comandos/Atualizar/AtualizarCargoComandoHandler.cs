using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Atualizar
{
    public class AtualizarCargoComandoHandler : IRequestHandler<AtualizarCargoComando, Resposta<CargoViewModel>>
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly IMapper _mapper;
        private readonly IAtualizarCargoComandoValidador _validador;

        public AtualizarCargoComandoHandler(ICargoRepositorio cargoRepositorio, IMapper mapper, IAtualizarCargoComandoValidador validador)
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
