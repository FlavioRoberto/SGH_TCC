using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Services;
using SHG.Data.Contexto;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Atualizar
{
    public class AtualizarCursoComandoHandler : IRequestHandler<AtualizarCursoComando, Resposta<Curso>>
    {
        private readonly ICursoRepositorio _repositorio;
        private readonly IValidador<AtualizarCursoComando> _validador;

        public AtualizarCursoComandoHandler(ICursoRepositorio repositorio, IValidador<AtualizarCursoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Curso>> Handle(AtualizarCursoComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<Curso>(erro);

            var curso = await _repositorio.Consultar(lnq => lnq.Codigo == request.Codigo);

            if (curso == null)
                return new Resposta<Curso>($"Não foi encontrado um curso com o código {request.Codigo}.");

            curso.Codigo = request.Codigo.Value;
            curso.Descricao = request.Descricao;

            var resultado = await _repositorio.Atualizar(curso);

            return new Resposta<Curso>(resultado);
        }
    }
}
