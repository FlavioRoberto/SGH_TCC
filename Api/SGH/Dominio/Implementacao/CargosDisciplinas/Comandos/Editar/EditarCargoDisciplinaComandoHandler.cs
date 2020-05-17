using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Editar
{
    public class EditarCargoDisciplinaComandoHandler : IRequestHandler<EditarCargoDisciplinaComando, Resposta<CargoDisciplinaViewModel>>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IValidador<EditarCargoDisciplinaComando> _validador;
        private readonly IMapper _mapper;

        public EditarCargoDisciplinaComandoHandler(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                   IValidador<EditarCargoDisciplinaComando> validador,
                                                   IMapper mapper)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<CargoDisciplinaViewModel>> Handle(EditarCargoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CargoDisciplinaViewModel>(erros);

            var cargoDisciplina = _mapper.Map<CargoDisciplina>(request);

            cargoDisciplina = await _cargoDisciplinaRepositorio.Atualizar(cargoDisciplina);

            var cargoDisciplinaViewModel = _mapper.Map<CargoDisciplinaViewModel>(cargoDisciplina);

            return new Resposta<CargoDisciplinaViewModel>(cargoDisciplinaViewModel);
        }
    }
}
