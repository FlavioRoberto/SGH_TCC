using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarDisciplinas
{
    public class ListarDisciplinasCurriculoConsultaHandler : IRequestHandler<ListarDisciplinasCurriculoConsulta, Resposta<List<CurriculoDisciplina>>>
    {
        private readonly ICurriculoDisciplinaRepositorio _repositorio;
        private readonly IListarDisciplinaCurriculoConsultaValidador _validador;

        public ListarDisciplinasCurriculoConsultaHandler(ICurriculoDisciplinaRepositorio repositorio, IListarDisciplinaCurriculoConsultaValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<List<CurriculoDisciplina>>> Handle(ListarDisciplinasCurriculoConsulta request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<List<CurriculoDisciplina>>(erros);

            var disciplinas = await _repositorio.Listar(lnq => lnq.CodigoCurriculo == request.CodigoCurriculo);

            return new Resposta<List<CurriculoDisciplina>>(disciplinas);
        }
    }
}
