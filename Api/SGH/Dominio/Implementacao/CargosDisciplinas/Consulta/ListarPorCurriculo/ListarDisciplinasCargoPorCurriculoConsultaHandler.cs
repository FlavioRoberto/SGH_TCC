using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo
{
    public class ListarDisciplinasCargoPorCurriculoConsultaHandler : IRequestHandler<ListarDisciplinaCargoPorCurriculoConsulta, Resposta<ICollection<CargoDisciplinaViewModel>>>
    {
        private readonly IValidador<ListarDisciplinaCargoPorCurriculoConsulta> _validador;

        public ListarDisciplinasCargoPorCurriculoConsultaHandler(IValidador<ListarDisciplinaCargoPorCurriculoConsulta> validador)
        {
            _validador = validador;
        }

        public Task<Resposta<ICollection<CargoDisciplinaViewModel>>> Handle(ListarDisciplinaCargoPorCurriculoConsulta request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return Task.FromResult(new Resposta<ICollection<CargoDisciplinaViewModel>>(erro));

            return Task.FromResult(new Resposta<ICollection<CargoDisciplinaViewModel>>(""));
        }
    }
}
