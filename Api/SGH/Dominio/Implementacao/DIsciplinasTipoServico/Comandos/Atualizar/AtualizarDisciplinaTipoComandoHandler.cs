using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Atualizar
{
    public class AtualizarDisciplinaTipoComandoHandler : IRequestHandler<AtualizarDisciplinaTipoServicoComando, Resposta<DisciplinaTipo>>
    {
        private readonly IDisciplinaTipoRepositorio _repositorio;

        public AtualizarDisciplinaTipoComandoHandler(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<DisciplinaTipo>> Handle(AtualizarDisciplinaTipoServicoComando request, CancellationToken cancellationToken)
        {
            var disciplinaTipo = new DisciplinaTipo
            {
                Codigo = request.Codigo,
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Atualizar(disciplinaTipo);
            return new Resposta<DisciplinaTipo>(resultado);
        }
    }
}
