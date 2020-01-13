using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cursos.Comandos.Atualizar
{
    public class AtualizarCursoComandoHandler : IRequestHandler<AtualizarCursoComando, Resposta<Curso>>
    {
        private readonly ICursoRepositorio _repositorio;

        public AtualizarCursoComandoHandler(IContexto contexto, ICursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Curso>> Handle(AtualizarCursoComando request, CancellationToken cancellationToken)
        {
            var curso = new Curso
            {
                Codigo = request.Codigo,
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Atualizar(curso);

            return new Resposta<Curso>(resultado);
        }
    }
}
