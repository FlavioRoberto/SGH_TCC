using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar
{
    public class CriarCargoDisciplinaComandoHandler : IRequestHandler<CriarCargoDisciplinaComando, Resposta<CargoDisciplinaViewModel>>
    {
        private readonly ICargoDisciplinaRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidador<CriarCargoDisciplinaComando> _validador;

        public CriarCargoDisciplinaComandoHandler(ICargoDisciplinaRepositorio cargoDisciplinaRepositorio, IMapper mapper, IValidador<CriarCargoDisciplinaComando> validador)
        {
            _repositorio = cargoDisciplinaRepositorio;
            _mapper = mapper;
            _validador = validador;
        }

        public async Task<Resposta<CargoDisciplinaViewModel>> Handle(CriarCargoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CargoDisciplinaViewModel>(erros);

            var disciplina = _mapper.Map<CargoDisciplina>(request);

            disciplina = await _repositorio.Criar(disciplina);

            var disciplinaViewModel = _mapper.Map<CargoDisciplinaViewModel>(disciplina);

            return new Resposta<CargoDisciplinaViewModel>(disciplinaViewModel);
        }
    }
}
