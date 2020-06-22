using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComandoHandler : CurriculoDisciplinaComandoHandlerBase, IRequestHandler<EditarCurriculoDisciplinaComando, Resposta<CurriculoDisciplinaViewModel>>
    {
        private readonly IValidador<EditarCurriculoDisciplinaComando> _validador;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;

        public EditarCurriculoDisciplinaComandoHandler(IValidador<EditarCurriculoDisciplinaComando> validador, 
                                                       ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                       IMapper mapper) : base(mapper)
        {
            _validador = validador;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
        }

        public async Task<Resposta<CurriculoDisciplinaViewModel>> Handle(EditarCurriculoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<CurriculoDisciplinaViewModel>(erro);

            var disciplina = _mapper.Map<CurriculoDisciplina>(request);

            disciplina.CurriculoDisciplinaPreRequisito = AdicionarPreRequisitors(request.PreRequisitos);

            disciplina = await _curriculoDisciplinaRepositorio.Atualizar(disciplina);

            var disciplianViewModel = _mapper.Map<CurriculoDisciplinaViewModel>(disciplina);

            return new Resposta<CurriculoDisciplinaViewModel>(disciplianViewModel);
        }
    }
}
