using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Consultas.ListarPorDescricao
{
    public class ListarPorDescricaoDisciplinaConsultaHandler : IRequestHandler<ListarPorDescricaoDisciplinaConsulta, Resposta<List<Disciplina>>>
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public ListarPorDescricaoDisciplinaConsultaHandler(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<List<Disciplina>>> Handle(ListarPorDescricaoDisciplinaConsulta request, CancellationToken cancellationToken)
        {
            var dados = await _repositorio.Listar(lnq => lnq.Descricao.ToLower().Contains(request.Descricao.ToLower()));
            return new Resposta<List<Disciplina>>(dados);
        }
    }
}
