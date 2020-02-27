using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cursos.Comandos.Remover
{
    public class RemoverCursoComandoHandler : IRequestHandler<RemoverCursoComando, Resposta<bool>>
    {
        private readonly ICursoRepositorio _repositorio;
        private readonly IRemoverCursoComandoValidador _validador;

        public RemoverCursoComandoHandler(ICursoRepositorio repositorio, IRemoverCursoComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverCursoComando request, CancellationToken cancellationToken)
        {
            string erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.CursoId);

            return new Resposta<bool>(resultado);
        }
    }
}
