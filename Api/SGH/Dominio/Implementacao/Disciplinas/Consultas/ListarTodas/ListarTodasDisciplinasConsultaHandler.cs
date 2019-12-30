using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarTodas
{   
    public class ListarTodasDisciplinasConsultaHandler : IRequestHandler<ListarTodasDisciplinasConsulta, Resposta<List<Disciplina>>>
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public ListarTodasDisciplinasConsultaHandler(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<List<Disciplina>>> Handle(ListarTodasDisciplinasConsulta request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<Disciplina>>(resultado);
        }
    }
}
