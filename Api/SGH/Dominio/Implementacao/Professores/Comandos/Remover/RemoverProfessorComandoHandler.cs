using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Remover
{
    public class RemoverProfessorComandoHandler : IRequestHandler<RemoverProfessorComando, Resposta<bool>>
    {
        private IProfessorRepositorio _repositorio;
        private IRemoverProfessorComandoValidador _validador;

        public RemoverProfessorComandoHandler(IProfessorRepositorio repositorio, IRemoverProfessorComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<bool>> Handle(RemoverProfessorComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (string.IsNullOrEmpty(erros))
                return new Resposta<bool>(erros);

            var resultado = await _repositorio.Remover(lnq => lnq.Codigo == request.ProfessorId);
            return new Resposta<bool>(resultado);
        }
    }
}