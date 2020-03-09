using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.ViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComandoHandler : IRequestHandler<EditarCurriculoDisciplinaComando, Resposta<CurriculoDisciplinaViewModel>>
    {
        private readonly IEditarCurriculoDisciplinaComandoValidador _validador;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;

        public EditarCurriculoDisciplinaComandoHandler(IEditarCurriculoDisciplinaComandoValidador validador, ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio)
        {
            _validador = validador;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
        }

        public async Task<Resposta<CurriculoDisciplinaViewModel>> Handle(EditarCurriculoDisciplinaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (string.IsNullOrEmpty(erro))
                return new Resposta<CurriculoDisciplinaViewModel>(erro);

            var entidade;

            var resultado = await _curriculoDisciplinaRepositorio.Editar();
        }
    }
}
