using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar
{
    public class CriarCurriculoDisciplinaComandoHandler : CurriculoDisciplinaComandoHandlerBase, IRequestHandler<CriarCurriculoDisciplinaComando, Resposta<CurriculoDisciplinaViewModel>>
    {
        private readonly IValidador<CriarCurriculoDisciplinaComando> _validador;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;

        public CriarCurriculoDisciplinaComandoHandler(IValidador<CriarCurriculoDisciplinaComando> validor,
                                                      ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                      IMapper mapper) : base(mapper)
        {
            _validador = validor;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
        }

        public async Task<Resposta<CurriculoDisciplinaViewModel>> Handle(CriarCurriculoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<CurriculoDisciplinaViewModel>(erros);

            var entidade = _mapper.Map<CurriculoDisciplina>(request);

            entidade.CurriculoDisciplinaPreRequisito = AdicionarPreRequisitors(request.PreRequisitos);

            var resultado = await _curriculoDisciplinaRepositorio.Criar(entidade);

            var viewModel = _mapper.Map<CurriculoDisciplinaViewModel>(resultado);

            return new Resposta<CurriculoDisciplinaViewModel>(viewModel);
        }

    }
}
