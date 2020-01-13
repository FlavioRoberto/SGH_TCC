using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cursos.Comandos.Criar
{
    public class CriarCursoComandoHandler : IRequestHandler<CriarCursoComando, Resposta<Curso>>
    {
        private readonly ICursoRepositorio _repositorio;

        public CriarCursoComandoHandler(ICursoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Curso>> Handle(CriarCursoComando request, CancellationToken cancellationToken)
        {
            var curso = new Curso
            {
                Descricao = request.Descricao
            };

            var cursoAdicionado = await _repositorio.Criar(curso);

            return new Resposta<Curso>(cursoAdicionado);
        }
    }
}
