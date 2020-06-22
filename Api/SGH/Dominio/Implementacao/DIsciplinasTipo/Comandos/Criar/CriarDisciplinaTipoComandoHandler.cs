using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Criar
{
    public class CriarDisciplinaTipoComandoHandler : IRequestHandler<CriarDisciplinaTipoComando, Resposta<DisciplinaTipo>>
    {
        private readonly IDisciplinaTipoRepositorio _repositorio;

        public CriarDisciplinaTipoComandoHandler(IDisciplinaTipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<DisciplinaTipo>> Handle(CriarDisciplinaTipoComando request, CancellationToken cancellationToken)
        {
            var disciplinaTipo = new DisciplinaTipo
            {
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Criar(disciplinaTipo);
            return new Resposta<DisciplinaTipo>(resultado);
        }
    }
}
