using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Base;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Editar
{
    public class EditarCargoDisciplinaComandoHandler : CargoDisciplinaComandoHandlerBase, IRequestHandler<EditarCargoDisciplinaComando, Resposta<CargoDisciplinaViewModel>>
    {
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly IValidador<EditarCargoDisciplinaComando> _validador;

        public EditarCargoDisciplinaComandoHandler(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                   IValidador<EditarCargoDisciplinaComando> validador,
                                                   IMapper mapper,
                                                   ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio) : base(mapper, curriculoDisciplinaRepositorio)
        {
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _validador = validador;
        }

        public async Task<Resposta<CargoDisciplinaViewModel>> Handle(EditarCargoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CargoDisciplinaViewModel>(erros);

            var cargoDisciplina = await MapearComandoParaDisciplina(request);

            cargoDisciplina = await _cargoDisciplinaRepositorio.Atualizar(cargoDisciplina);

            var cargoDisciplinaViewModel = _mapper.Map<CargoDisciplinaViewModel>(cargoDisciplina);

            return new Resposta<CargoDisciplinaViewModel>(cargoDisciplinaViewModel);
        }
    }
}
