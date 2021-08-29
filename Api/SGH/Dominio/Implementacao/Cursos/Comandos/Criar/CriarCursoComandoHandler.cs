using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Criar
{
    public class CriarCursoComandoHandler : IRequestHandler<CriarCursoComando, Resposta<Curso>>
    {
        private readonly ICursoRepositorio _repositorio;
        private readonly IValidador<CriarCursoComando> _validador;

        public CriarCursoComandoHandler(ICursoRepositorio repositorio, IValidador<CriarCursoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Curso>> Handle(CriarCursoComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<Curso>(erro);

            var curso = new Curso
            {
                Descricao = request.Descricao
            };

            var cursoAdicionado = await _repositorio.Criar(curso);

            return new Resposta<Curso>(cursoAdicionado);
        }
    }
}
