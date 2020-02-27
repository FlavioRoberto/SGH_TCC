using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComandoHandler : IRequestHandler<CriarCargoComando,Resposta<CargoViewModel>>
    {
        private readonly ICargoRepositorio _repositorioCargo;
        private readonly ICriarCargoComandoValidador _validador;
        private readonly IMapper _mapper;

        public CriarCargoComandoHandler(ICargoRepositorio repositorio, 
                                        ICriarCargoComandoValidador validador,
                                        IMapper mapper)
        {
            _repositorioCargo = repositorio;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<CargoViewModel>> Handle(CriarCargoComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CargoViewModel>(erros);

            var entidade = _mapper.Map<Cargo>(request);

            var resultado = await _repositorioCargo.Criar(entidade);

            var viewModel = _mapper.Map<CargoViewModel>(resultado);

            return new Resposta<CargoViewModel>(viewModel);
        }
    }
}
