using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Atualizar
{
    public class AtualizarDisciplinaTipoComandoHandler : IRequestHandler<AtualizarDisciplinaTipoComando, Resposta<DisciplinaTipo>>
    {
        private readonly IDisciplinaTipoRepositorio _repositorio;

        public AtualizarDisciplinaTipoComandoHandler(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<DisciplinaTipo>> Handle(AtualizarDisciplinaTipoComando request, CancellationToken cancellationToken)
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
