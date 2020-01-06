using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoHandler : IRequestHandler<CriarUsuarioComando, Resposta<Usuario>>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public CriarUsuarioComandoHandler(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Usuario>> Handle(CriarUsuarioComando request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
