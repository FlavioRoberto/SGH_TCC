using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar
{
    public class CriarTurnoComandoHandler : IRequestHandler<CriarTurnoComando, Resposta<Turno>>
    {
        private readonly ITurnoRepositorio _repositorio;

        public CriarTurnoComandoHandler(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Turno>> Handle(CriarTurnoComando request, CancellationToken cancellationToken)
        {
            var turno = new Turno { 
                Descricao = request.Descricao
            };

            var resultado = await _repositorio.Criar(turno);

            return new Resposta<Turno>(resultado);
        }
    }
}
