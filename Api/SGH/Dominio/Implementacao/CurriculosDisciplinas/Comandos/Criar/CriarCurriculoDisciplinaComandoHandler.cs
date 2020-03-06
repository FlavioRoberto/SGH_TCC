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

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar
{
    public class CriarCurriculoDisciplinaComandoHandler : IRequestHandler<CriarCurriculoDisciplinaComando, Resposta<CurriculoDisciplinaViewModel>>
    {
        private readonly ICriarCurriculoDisciplinaComandoValidador _validador;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        private readonly IMapper _mapper;

        public CriarCurriculoDisciplinaComandoHandler(ICriarCurriculoDisciplinaComandoValidador validor,
                                                      ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                      IMapper mapper)
        {
            _validador = validor;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<CurriculoDisciplinaViewModel>> Handle(CriarCurriculoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CurriculoDisciplinaViewModel>(erros);

            var entidade = _mapper.Map<CurriculoDisciplina>(request);

            var resultado = await _curriculoDisciplinaRepositorio.Criar(entidade);

            var viewModel = _mapper.Map<CurriculoDisciplinaViewModel>(resultado);

            return new Resposta<CurriculoDisciplinaViewModel>(viewModel);
        }
    }
}
